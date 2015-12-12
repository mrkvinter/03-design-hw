using System.Collections.Generic;
using System.Windows.Forms;
using WordsCloud.ViewWordsCloud;

namespace WordsCloud.Client
{
    class GuiClient
        : Form, IClient
    {
        public GuiClient(IView viewer)
        {
            Name = "GUI";
        }
        
        public void Run(List<Word> words)
        {
            Application.Run(this);
        }
    }
}
