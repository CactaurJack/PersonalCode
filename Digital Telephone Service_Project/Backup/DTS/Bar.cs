using System;
using System.Collections.Generic;
using System.Text;

namespace DTS
{

    public interface Bar
    {
        //Interface meathod declarations
        bool IsBar(string[] inNumber);
        bool Check(string areaCode);
        bool Check(string[] inNumber);
    }
}
