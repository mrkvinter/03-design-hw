using System.Collections.Generic;
using System.Windows.Forms;

namespace WordsCloud.Client
{
    class GuiClient
        : Form, IClient
    {
        public GuiClient()
        {
            Name = "GUI";
        }
        
        public void Run(List<Word> words)
        {
            Application.Run(this);
        }
    }
}
