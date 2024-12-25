using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyreaRPG.Characters
{
    internal class PersonalityData
    {
        public static readonly List<string> Traits = new()
        {
            "Calm and analytical",
            "Passionate and fiery",
            "Inquisitive and curious",
            "Stubborn and determined",
            "Cheerful and optimistic",
            "Reserved and contemplative",
            "Loyal and trustworthy",
            "Charismatic and persuasive",
            "Resourceful and practical",
            "Cunning and opportunistic",
            "Ambitious and driven",
            "Creative and imaginative",
            "Hardworking and diligent",
            "Honest and straightforward",
            "Reckless and impulsive",
            "Patient and calculated"
        };

        public static readonly List<string> Ideals = new()
        {
            "Freedom above all else",
            "Justice for the wronged",
            "Loyalty to one’s comrades",
            "Power as the ultimate goal",
            "Knowledge as the key to greatness",
            "Community and shared success",
            "Order and structure",
            "Personal gain and wealth",
            "Creativity and innovation",
            "Adventure and discovery",
            "Protecting the weak",
            "Pursuit of truth",
            "Harmony with nature",
            "Honor and reputation",
            "Preservation of tradition"
        };

        public static readonly List<string> Bonds = new()
        {
             "A loyal friend or family member",
            "A sacred relic or heirloom",
            "A mentor who inspired them",
            "Their hometown or home region",
            "A lost love they vow to honor",
            "A faction or guild they belong to",
            "A ship they hold dear",
            "A specific cause or goal",
            "A grudge against an enemy",
            "A child or dependent they care for",
            "A deep connection to nature",
            "A pet or companion animal",
            "A treasure they seek to protect",
            "A duty or promise they must fulfill",
            "A secret they guard fiercely"
        };

        public static readonly List<string> Flaws = new()
        {
            "Quick to anger",
            "Distrustful of others",
            "Addiction to vices like gambling or drinking",
            "Prideful and unwilling to admit fault",
            "Impatient and rash",
            "Obsessed with wealth or power",
            "Fearful of conflict or danger",
            "Easily manipulated by flattery",
            "Vindictive and holds grudges",
            "Overconfident in their abilities",
            "Tendency to betray others",
            "Naive and overly trusting",
            "Cowardly in critical moments",
            "Recklessly adventurous",
            "Egotistical and self-centered"
        };
    }
}
