using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.SystemManagement.spell_checker
{
    public interface ISpellChecker
    {
        List<string> Correct(string word);
    }
}
