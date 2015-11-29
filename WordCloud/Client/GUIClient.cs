using System.Windows.Forms;
using WordsCloud.Algorithm;
using WordsCloud.ViewWordsCloud;

namespace WordsCloud.Client
{
    class GuiClient
        : Form, IClient
    {
        public GuiClient(IAlgorithm algo, IView viewer)
        {
            Name = "GUI";
        }
        
        public void Run()
        {
            Application.Run(this);
        }
    }
}
