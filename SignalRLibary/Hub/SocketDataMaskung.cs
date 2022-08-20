using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Hubs;
using SignalRLibary.ModelUserSignalR;
using SignalRLibary.SignalR;

namespace SignalRLibary.Hub
{
    [HubName("SocketDataMasking")]
    public class SocketDataMasking : Microsoft.AspNet.SignalR.Hub // this "Hub" is hooked by SignalR and is important. 
    {
        private static readonly BasicChatConnect<UsersContext> Connections = new BasicChatConnect<UsersContext>();
        /// <summary>
        /// Переназначеный класс подключение пользователя
        /// </summary>
        /// <returns></returns>
        public override Task OnConnected()
        {

            var user = new UsersContext()
            {
                IdUser = Convert.ToInt32(Context.QueryString["iduser"]),
                NameUser = Context.QueryString["user"],
                PersonnelNumber = Context.QueryString["personnelNumber"]
            };
            try
            {
                Connections.Add(user, Context.ConnectionId);
                HelloUser("Добро пожаловать пользователь: " + user.NameUser, Context.ConnectionId);
                Loggers.Log4NetLogger.Info(new Exception("Подключился пользователь: Имя - " + user.NameUser + " Номер - " + user.PersonnelNumber + " Контекст - " + Context.ConnectionId));
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
            return base.OnConnected();
        }
        /// <summary>
        /// Метод отключение пользователя!!!
        /// </summary>
        /// <param name="stopCalled"></param>
        /// <returns></returns>
        public override Task OnDisconnected(bool stopCalled)
        {
            var user = new UsersContext()
            {
                IdUser = Convert.ToInt32(Context.QueryString["iduser"]),
                NameUser = Context.QueryString["user"],
                PersonnelNumber = Context.QueryString["personnelNumber"]
            };
            Loggers.Log4NetLogger.Info(new Exception("Отключился пользователь: Имя - " + user.NameUser + " Номер - " + user.PersonnelNumber + " Контекст - " + Context.ConnectionId));
            Connections.Remove(user, Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }

        /// <summary>
        /// Переподключение
        /// </summary>
        /// <returns></returns>
        public override Task OnReconnected()
        {
            var user = new UsersContext()
            {
                IdUser = Convert.ToInt32(Context.QueryString["iduser"]),
                NameUser = Context.QueryString["user"],
                PersonnelNumber = Context.QueryString["personnelNumber"]
            };
            if (!Connections.GetConnections(user).Contains(Context.ConnectionId))
            {
                Connections.Add(user, Context.ConnectionId);
            }
            Loggers.Log4NetLogger.Info(new Exception("Переподключился пользователь: Имя - " + user.NameUser + " Номер - " + user.PersonnelNumber + " Контекст - " + Context.ConnectionId));
            return base.OnReconnected();
        }

        public void HelloUser(string hellouser, string conectionId)
        {
            Clients.Client(conectionId).HelloUser(hellouser);
        }
    }
}
