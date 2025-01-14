using ChatAppli.MVVM.Core;
using ChatAppli.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppli.MVVM.ViewModel
{
    class MainViewModel
    {
        

        private Server _server;
        public MainViewModel()
        {
            _server = new Server();
         
        }
    }
}
