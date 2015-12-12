using System.Collections.Generic;
using WordsCloud.Algorithm;

namespace WordsCloud.ViewWordsCloud
{
    public interface IView
    {
        void CreateImage(List<Word> algo);
    }
}
