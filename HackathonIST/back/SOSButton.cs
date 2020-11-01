using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace HackathonIST.back
{
    class SOSButton
    {
        public void SOSCall()
        {
            //Дима, прикрути запрос на сервер
            PhoneDialer.Open("+79047577207");
        }
    }
}
