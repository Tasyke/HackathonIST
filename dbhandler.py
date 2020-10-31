#import psycopg2
import sqlite3
import hashlib

DB_NAME = ""
DB_USER = ""
DB_PASSWORD = ""
DB_HOST = "localhost"

def connectToDatabase():
    # conn = psycopg2.connect(dbname=DB_NAME, user=DB_USER, password=DB_PASSWORD, host=DB_HOST)
    conn = sqlite3.connect("test.db")
    cursor = conn.cursor()
    return conn, cursor

def closeConnection(conn):
    conn.close()

def createDefaultTables():
    conn, cursor = connectToDatabase()

    # Сюда запросы на создание и т.п.
    cursor.execute("CREATE TABLE construction (constructionID INTEGER PRIMARY KEY AUTOINCREMENT, geolocation VARCHAR(32), mainName VARCHAR(100), govContract VARCHAR(64), buildArea VARCHAR(24), customer VARCHAR(64), generalContractor VARCHAR(64), buildAllowment VARCHAR(64));");
    cursor.execute("CREATE TABLE builder (builderID INTEGER PRIMARY KEY AUTOINCREMENT, firstName VARCHAR(64), surName VARCHAR(64), lastName VARCHAR(64), phoneNumber VARCHAR(20), constructionID INTEGER, companyName VARCHAR(255), email VARCHAR(64), timestampID INTEGER);");
    cursor.execute("CREATE TABLE users (builderID INTEGER PRIMARY KEY AUTOINCREMENT, login VARCHAR(255), password VARCHAR(255))");
    cursor.execute("CREATE TABLE timestamps (timestampID INTEGER PRIMARY KEY AUTOINCREMENT, timeStart TIME, timeEnd TIME, startWorkDate DATE, endWorkDate DATE, isDone INT, builderID INTEGER)");

    conn.commit()
    closeConnection(conn)

# ---------- [Базовые запросы] ----------
def getAuthData():
    conn, cursor = connectToDatabase()
    cursor.execute("SELECT * FROM users")
    data = cursor.fetchall()
    closeConnection(conn)
    if not data:
        return None
    else: 
        return data

def getBuilderInfo():
    conn, cursor = connectToDatabase()
    cursor.execute("SELECT * FROM builder")
    data = cursor.fetchall()
    closeConnection(conn)
    if not data:
        return None
    else: 
        return data

def getConstructionInfo():
    conn, cursor = connectToDatabase()
    cursor.execute("SELECT * FROM construction")
    data = cursor.fetchall()
    closeConnection(conn)
    if not data:
        return None
    else: 
        return data

def getTimeStampsInfo():
    conn, cursor = connectToDatabase()
    cursor.execute("SELECT * FROM timestamps")
    data = cursor.fetchall()
    closeConnection(conn)
    if not data:
        return None
    else: 
        return data
    
# ---------- [Особые запросы] ----------
def registerUser(userData):
    splitData = userData.split(';')
    
    userFullName = splitData[0] + ' ' + splitData[1] + ' ' + splitData[2]
    userPhone = splitData[3]
    userWork = splitData[4]
    userMail = splitData[5]
    userPassword = splitData[6]
    userLogin = userPhone

    conn, cursor = connectToDatabase()
    
    result = False

    cursor.execute("SELECT * FROM users WHERE login = (?)", (userLogin,))
    user = cursor.fetchone()
    if not user:
        h = hashlib.md5(userPassword.encode('utf-8'))
        hashPass = h.hexdigest()
        cursor.execute("INSERT INTO builder (firstName, surName, lastName, phoneNumber, companyName, email) VALUES ((?), (?), (?), (?), (?), (?))", (splitData[0], splitData[1], splitData[2], userPhone, userWork, userMail))
        cursor.execute("SELECT builderID FROM users WHERE login = (?)", (userLogin,))
        bID = cursor.fetchone()
        cursor.execute("INSERT INTO users (builderID, login, password) VALUES ((?), (?), (?))", (bID, userLogin, hashPass))
        conn.commit()
        result = True
    
    closeConnection(conn)
    return result

def authUser(userData):
    temp = userData.split(';')
    login = temp[0]
    password = temp[1]

    conn, cursor = connectToDatabase()
    cursor.execute("SELECT * FROM users WHERE login = (?)", (login,))
    user = cursor.fetchone()
    h = hashlib.md5(password.encode('utf-8'))
    hashPass = h.hexdigest()
    closeConnection(conn)

    if (not user):
        return False

    if user[2] == hashPass:
        return True
    else:
        return False

def startWork(workerID, dateTime):
    conn, cursor = connectToDatabase()
    
    cursor.execute("SELECT * FROM timestamps WHERE builderID = (?) AND startWorkDate = (?)", (workerID, dateTime.date()))
    timeStampData = cursor.fetchone()

    if not timeStampData:
        # Добавляем новую рабочую смену
        cursor.execute("INSERT INTO timestamps (timeStart, startWorkDate, builderID, isDone) VALUES ((?), (?), (?), (?))", (dateTime.strftime("%H:%M:%S"), dateTime.date(), workerID, 0))
        conn.commit()
        # Достаём ID этой смены
        cursor.execute("SELECT timestampID FROM timestamps WHERE builderID = (?) AND startWorkDate = (?)", (workerID, dateTime.date()))
        timeStampID = cursor.fetchone()[0]
        # Устанавливаем последнюю смену рабочего
        cursor.execute("UPDATE builder SET timestampID = (?) WHERE builderID = (?)", (timeStampID, workerID))
        conn.commit()
        closeConnection(conn)
        return "Work started!"
    else:
        closeConnection(conn)
        # Либо создать новую, либо что-то придумать 
        return "Work is already started!"

def endWork(workerID, dateTime):
    conn, cursor = connectToDatabase()

    # Берём последнюю начатую рабочую смену строителя
    cursor.execute("SELECT timestampID FROM builder WHERE builderID = (?)", (workerID, ))
    timestampID = cursor.fetchone()[0]

    cursor.execute("SELECT * FROM timestamps WHERE timestampID = (?)", (timestampID, ))
    timeStampData = cursor.fetchone()
    
    if not timeStampData:
        # Заканчиваешь смену, а её нет, очень странно, но надо ещё подумать
        closeConnection(conn)
        return "Didn't find work timestamp"
    else:
        cursor.execute("UPDATE timestamps SET timeEnd = (?), endWorkDate = (?), isDone = (?) WHERE builderID = (?) AND timestampID = (?)", (dateTime.strftime("%H:%M:%S"), dateTime.date(), 1, workerID, timestampID))
        conn.commit()
        closeConnection(conn)
        return "Work ended"

