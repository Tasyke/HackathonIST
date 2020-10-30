using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HackathonIST
{
    public partial class HackathonIST : Shell
    {
        public HackathonIST()
        {
            Items.Add(new FlyoutItem
            {
                Title = "Авторизация",
                Items =
                {
                    new Tab
                    {
                        Items = { new ShellContent {Content = new Authorization() } }
                }
            }
            });
        }
    }
}