using System.Collections.Generic;

namespace ECommerceSystem.DomainLayer.SystemManagement.spell_checker
{
    public interface ISpellChecker
    {
        List<string> Correct(string word);
    }
}