using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud.ViewTagCloud
{
    public interface IView
    {
        void CreateImage(TagContainer tagContainer);
    }
}
