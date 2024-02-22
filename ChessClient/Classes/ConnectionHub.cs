using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClient.Classes
{
    public class ConnectionHub
    {
        /// <summary>
        /// Esta istancia é criada apenas quando é acessada,pois está a usar o Lazy do FrameWork que basicamente so cria a class quando a usar
        /// Assim o peso do programa é reduzido
        /// para invocar esta class para uso é -> ConnectionHub [nome] = ConnectionHub.Instance;
        /// </summary>
        private static readonly Lazy<ConnectionHub> _instance = new Lazy<ConnectionHub>(() => new ConnectionHub("https://localhost:7286"));

        public static ConnectionHub Instance => _instance.Value;

        /// <summary>
        /// Está variavel é o canal a qual estamos conectados
        /// </summary>
        public GrpcChannel Channel;

        /// <summary>
        /// Este é o serviço de Greeting Criado de Olá
        /// </summary>
        public Greeter.GreeterClient GreeterClient;

        public Acesser.AcesserClient AcesserClient { get; set; }
        

        // É corrido apenas uma vez
        private ConnectionHub(string connectionString)
        {
            Channel = GrpcChannel.ForAddress(connectionString);
            GreeterClient = new Greeter.GreeterClient(Channel);
            AcesserClient = new Acesser.AcesserClient(Channel);
        }

      

    }

}
