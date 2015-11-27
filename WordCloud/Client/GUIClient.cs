using System.Windows.Forms;

namespace WordsCloud.Client
{
    class GuiClient
        : Form, IClient
    {
        public GuiClient(WordsContainer container = null)
        {
            Name = "GUI";
        }
        
        public void Run()
        {
            Application.Run(this);
        }
    }
}
