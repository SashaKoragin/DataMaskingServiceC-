using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using DataBaseDataMasking.LogicDataBase.ModelDto.AutoIdentification;

namespace DataMasking.Service
{
    [ServiceContract(SessionMode = SessionMode.NotAllowed)]
    public interface IServiceDataMasking
    {
        /// <summary>
        /// Привет мир!!!
        /// http://localhost:8182/DataMasking/HelloWorld
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, UriTemplate = "/HelloWorld", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string HelloWorld();
        /// <summary>
        /// Авторизация пользователя
        /// http://localhost:8182/DataMasking/AutoIdentification
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/AutoIdentification", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        UserDtoLoginAndPassword AutoIdentification(UserDtoLoginAndPassword userDtoLoginAndPassword);
    }
}
