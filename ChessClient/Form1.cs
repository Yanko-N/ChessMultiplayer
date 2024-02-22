using ChessClient.Classes;
using Grpc.Net.Client;

namespace ChessClient
{
    public partial class Form1 : Form
    {
        ConnectionHub connectionHub = ConnectionHub.Instance;
        Client player = new Client();
        Match match = new Match();

        GameMatch chessGame;
        bool startedGame = false;

        //Variavel auxiliar para uma animação
        int ponto = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Label 1 é a label para inserir o nome inicialmente
            label1.Text = "Insere o teu nome:";
            ChessPanel.Visible = false;


        }


        //Conenct Button Clicked
        private async void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBox1.Text))
                return;

            //TextBox é onde será inserido o nome do player
            var playerName = textBox1.Text;

            try
            {
                //está resposta é basicamente um client kk
                var response = await connectionHub.AcesserClient.ConnectAsync(new ConnectRequest { Name = playerName });
                player.Name = response.Name;
                player.InGame = response.InGame;
                player.Id = response.Id;

                //making invisible the shit stuff
                textBox1.Visible = false;
                button1.Visible = false;


                textBox1.Text = "";
            }
            catch
            {
                //provavelmente o erro será da conexão
                label1.Text = "Error Acessing the Server, try again";
                textBox1.Text = playerName;
                button1.Visible = true;
                button1.Text = "Retentar";
            }




        }

        private void UpdateMethod(object sender, EventArgs e)
        {
            if (!player.InGame && !String.IsNullOrWhiteSpace(player.Id))
            {
                //Aqui quer dizer que acabou de ser conectado
                //Logo teremos de fazer o player procurar uma partida :)
                if (ponto == 0)
                {
                    label1.Text = "Searching for a MATCH";
                    ponto++;
                }
                else if (ponto == 1)
                {
                    label1.Text = "Searching for a MATCH.";
                    ponto++;
                }
                else if (ponto == 2)
                {
                    label1.Text = "Searching for a MATCH..";
                    ponto++;
                }
                else if (ponto == 3)
                {
                    label1.Text = "Searching for a MATCH...";
                    ponto = 0;
                }

                try
                {
                    var matchResponse = connectionHub.AcesserClient.AskMatch(new MatchRequest
                    {
                        ClientId = player.Id
                    });

                    if (!String.IsNullOrWhiteSpace(matchResponse.MatchId))
                    {
                        player.InGame = true;
                        match = new Match
                        {
                            Id = matchResponse.MatchId,
                            BoardInNotation = matchResponse.BoardInNotation,
                            IsFirst = matchResponse.YourTurn
                        };
                        PrepareGameWindow();
                    }
                }
                catch
                {
                    label1.Text = "Error Acessing the Server, try again";

                }

            }
            else if (player.InGame && !String.IsNullOrWhiteSpace(player.Id))
            {
                //Se o jogador já esta na partida e o jogo ainda não começou
                if (!startedGame)
                {
                    //crio o jogo de xadrez
                    chessGame = new GameMatch(match.BoardInNotation, Controls, match.IsFirst);
                    startedGame = true;
                }
                else
                {
                    //Aqui já estamos logados e temos um jogo
                    //Será rodado o jogo aqui
                    GameUpdateMethod();
                }

            }
        }

        //Aqui será programado a partida de Xadrez
        void GameUpdateMethod()
        {

        }
        void PrepareGameWindow()
        {
            panel1.Visible = false;
            ChessPanel.Visible = true;
        }

        
    }
}
