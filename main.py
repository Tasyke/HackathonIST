import sys
import asyncio
import select 
import socket
import datetime
import json

import dbhandler

SECRET_HANDSHAKE = "SECRET_HANDSHAKE"
SERVER_ADDRESS = ("95.142.47.122", 8686)
MAX_CONNECTIONS = 50
HEADER_LENGTH = 24
REQUEST_LENGTH = 24

INPUTS = list()
OUTPUTS = list()
ANSWERS = dict()

def check_client_input(input):
    header = input[:HEADER_LENGTH].strip()
    request = input[HEADER_LENGTH:HEADER_LENGTH + REQUEST_LENGTH].strip()
    body = input[HEADER_LENGTH + REQUEST_LENGTH:]
    
    print(header)
    print(request)
    print(body)

    if (header == "handshake"):
        return SECRET_HANDSHAKE
    elif (header == "cmd"):
        return execute_cmd(request, body)
    elif (header == "sos"):
        return SOSSignal()
    else:
        return "Unidentified Request :("

def SOSSignal():
    # Отправляем сигнал диспетчерам
    pass

def execute_cmd(cmd, body):
    print(f"Trying new command: ", cmd)

    if (cmd == "GetDatabase"):
        data = dbhandler.getBuilderInfo()
        if not data:
            return "Empty"

        request = '{"workers":['
        for user in data:
            request += '{' + f"\"userID\":{user[0]},\"userName\":\"{user[1]}\"" + '},'
        request = request[:-1]
        request += ']}'
        return request

    elif (cmd == "RegisterUser"):
        reg = dbhandler.registerUser(body)
        if reg:
            return "True"
        else:
            return "False"

    elif (cmd == "AuthUser"):
        auth = dbhandler.authUser(body)
        if auth:
            return "True"
        else:
            return "False"
            
    elif (cmd == "StartWork"):
        request = dbhandler.startWork(int(body), datetime.datetime.now())
        return request
    elif (cmd == "EndWork"):
        request = dbhandler.endWork(int(body), datetime.datetime.now())
        return request
    else:
        return "Not accepted command"

def get_non_blocking_server_socket(server_data):
    
    server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server.setblocking(0)
    server.bind(server_data)
    server.listen(MAX_CONNECTIONS)

    return server

def handle_readables(readables, server):

    for resource in readables:
        if resource is server:
            connection, client_address = resource.accept()
            connection.setblocking(0)
            INPUTS.append(connection)
            
            print("[TIMESTAMP]: " + str(datetime.datetime.now()))
            print (f"\nNew client: {client_address}")
        else:
            data = ""
            
            try: 
                data = resource.recv(4096)
            except ConnectionResetError:
                print(f"Connection Error")

            if data:
                print(f"Get data from client: {str(data, encoding='utf-8')}")
                result = check_client_input(str(data, encoding='utf-8'))
                ANSWERS.update({resource: result})
                if resource not in OUTPUTS:
                    OUTPUTS.append(resource)
            else: 
                clear_resource(resource)

def clear_resource(resource):

    if resource in OUTPUTS:
        OUTPUTS.remove(resource)
    if resource in INPUTS:
        INPUTS.remove(resource)
    if resource in ANSWERS:
        ANSWERS.pop(resource)
    
    resource.close();                
    print("Closed connection with client")

def handle_writables(writables):

    for resource in writables:
        try: 
            print("Send data to client...")
            resource.sendall(bytes(f"{ANSWERS.pop(resource)}", encoding='UTF-8'))
            OUTPUTS.remove(resource)
        except OSError:
            clear_resource(resource)

if __name__ == '__main__':

    server_socket = get_non_blocking_server_socket(SERVER_ADDRESS)
    INPUTS.append(server_socket)
    
    print("Server is running as u wish sir!")
    try: 
        while INPUTS: 
            readables, writables, exceptional = select.select(INPUTS, OUTPUTS, INPUTS)
            handle_readables(readables, server_socket)
            handle_writables(writables)
    except KeyboardInterrupt:
        clear_resource(server_socket)
        print("Sir, I just get stopped! Thanks for using bro!")