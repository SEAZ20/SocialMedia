using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Core.Exeptions
{
    public class BusinessExeption : Exception
    {
        public BusinessExeption()
        {

        }
        public BusinessExeption(string mensaje): base(mensaje)
        {
                
        }
    }
}
