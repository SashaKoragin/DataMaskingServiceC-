using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseDataMasking.LogicDataBase.ModelDto.AutoIdentification;

namespace DataMasking.Service
{
   public class ServiceDataMasking : IServiceDataMasking
    {

        public string HelloWorld()
        {
            return "Привет мир!!!";
        }

        public UserDtoLoginAndPassword AutoIdentification(UserDtoLoginAndPassword userDtoLoginAndPassword)
        {
            userDtoLoginAndPassword.IsError = false;
            userDtoLoginAndPassword.ErrorMessage = null;

            userDtoLoginAndPassword.GroupRuleServer = new[] {"Admin", "Registrator"};
            userDtoLoginAndPassword.NameUser = "Александр";
            return userDtoLoginAndPassword;
        }
    }
}
