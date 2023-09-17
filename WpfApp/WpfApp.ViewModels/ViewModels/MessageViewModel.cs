using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.ViewModels.ViewModels
{
    public class MessageViewModel
    {
        public static readonly Messenger MessengerInstance = new Messenger();
    }
}
