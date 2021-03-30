using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace chordprogression
{
    class PitchArrangement
    {

        Regex regexC = new Regex(@"^(C)(M7|7|dim)?(/[A-Z][#|b]?|sus4|add9|dim[7|9]?|b5|aug|6)?$");
        Regex regexF = new Regex(@"^(F)(M7|7|dim)?(/[A-Z][#|b]?|sus4|add9|dim[7|9]?|b5|aug|6)?$");
        Regex regexBb = new Regex(@"^(Bb|A#)(M7|7|dim)?(/[A-Z][#|b]?|sus4|add9|dim[7|9]?|b5|aug|6)?$");
        Regex regexEb = new Regex(@"^(Eb|D#)(M7|7|dim)?(/[A-Z][#|b]?|sus4|add9|dim[7|9]?|b5|aug|6)?$");
        Regex regexAb = new Regex(@"^(Ab|G#)(M7|7|dim)?(/[A-Z][#|b]?|sus4|add9|dim[7|9]?|b5|aug|6)?$");
        Regex regexDb = new Regex(@"^(Db|C#)(M7|7|dim)?(/[A-Z][#|b]?|sus4|add9|dim[7|9]?|b5|aug|6)?$");
        Regex regexGb = new Regex(@"^(Gb|F#)(M7|7|dim)?(/[A-Z][#|b]?|sus4|add9|dim[7|9]?|b5|aug|6)?$");
        Regex regexB = new Regex(@"^(B)(M7|7|dim)?(/[A-Z][#|b]?|sus4|add9|dim[7|9]?|b5|aug|6)?$");
        Regex regexE = new Regex(@"^(E)(M7|7|dim)?(/[A-Z][#|b]?|sus4|add9|dim[7|9]?|b5|aug|6)?$");
        Regex regexA = new Regex(@"^(A)(M7|7|dim)?(/[A-Z][#|b]?|sus4|add9|dim[7|9]?|b5|aug|6)?$");
        Regex regexD = new Regex(@"^(D)(M7|7|dim)?(/[A-Z][#|b]?|sus4|add9|dim[7|9]?|b5|aug|6)?$");
        Regex regexG = new Regex(@"^(G)(M7|7|dim)?(/[A-Z][#|b]?|sus4|add9|dim[7|9]?|b5|aug|6)?$");
        Regex regexAm = new Regex(@"^(Am)(7|M7)?(/[A-Z][#|b]?|sus4|add9|dim[7|9]?|b5|aug|6)?");
        Regex regexDm = new Regex(@"^(Dm)(7|M7)?(/[A-Z][#|b]?|sus4|add9|dim[7|9]?|b5|aug|6)?");
        Regex regexGm = new Regex(@"^(Gm)(7|M7)?(/[A-Z][#|b]?|sus4|add9|dim[7|9]?|b5|aug|6)?");
        Regex regexCm = new Regex(@"^(Cm)(7|M7)?(/[A-Z][#|b]?|sus4|add9|dim[7|9]?|b5|aug|6)?");
        Regex regexFm = new Regex(@"^(Fm)(7|M7)?(/[A-Z][#|b]?|sus4|add9|dim[7|9]?|b5|aug|6)?");
        Regex regexBbm = new Regex(@"^(A#m|Bbm)(7|M7)?[/|add|b5|aug]?[A-Z]?[#|b]?");
        Regex regexEbm = new Regex(@"^(D#m|Ebm)(7|M7)?[/|add|b5|aug]?[A-Z]?[#|b]?");
        Regex regexAbm = new Regex(@"^(G#m|Abm)(7|M7)?[/|add|b5|aug]?[A-Z]?[#|b]?");
        Regex regexDbm = new Regex(@"^(C#m|Dbm)(7|M7)?[/|add|b5|aug]?[A-Z]?[#|b]?");
        Regex regexGbm = new Regex(@"^(F#m|Gbm)(7|M7)?[/|add|b5|aug]?[A-Z]?[#|b]?");
        Regex regexBm = new Regex(@"^(Bm)(7|M7)?[/|add|b5|aug]?[A-Z]?[#|b]?");
        Regex regexEm = new Regex(@"^(Em)(7|M7)?[/|add|b5|aug]?[A-Z]?[#|b]?");

        public string Sharp(string chord)
        {
            if (regexC.IsMatch(chord) == true && chord.Contains("Cm") == false)
            {
                chord.Replace("C", "C#");
                return chord;
            }
            else if (regexF.IsMatch(chord) == true && chord.Contains("Fm") == false)
            {
                chord.Replace("F", "F#");
                return chord;
            }
            else if (regexBb.IsMatch(chord) == true && chord.Contains("Bbm") == false && chord.Contains("A#m") == false)
            {
                chord.Replace("Bb", "B");
                chord.Replace("A#", "B");
                return chord;
            }
            else if (regexEb.IsMatch(chord) == true && chord.Contains("Ebm") == false && chord.Contains("D#m") == false)
            {
                chord.Replace("Eb", "E");
                chord.Replace("D#", "E");
                return chord;
            }
            else if (regexAb.IsMatch(chord) == true && chord.Contains("Abm") == false && chord.Contains("G#m") == false)
            {
                chord.Replace("Ab", "A");
                chord.Replace("G#", "A");
                return chord;
            }
            else if (regexDb.IsMatch(chord) == true && chord.Contains("Dbm") == false && chord.Contains("C#m") == false)
            {
                chord.Replace("Db", "D");
                chord.Replace("C#", "D");
                return chord;
            }
            else if (regexGb.IsMatch(chord) == true && chord.Contains("Gbm") == false && chord.Contains("F#m") == false)
            {
                chord.Replace("Gb", "G");
                chord.Replace("F#", "G");
                return chord;
            }
            else if (regexB.IsMatch(chord) == true && chord.Contains("Bm") == false)
            {
                chord.Replace("B", "C");
                return chord;
            }
            else if (regexE.IsMatch(chord) == true && chord.Contains("Em") == false)
            {
                chord.Replace("E", "F");
                return chord;
            }
            else if (regexA.IsMatch(chord) == true && chord.Contains("Am") == false)
            {
                chord.Replace("A", "A#");
                return chord;
            }
            else if (regexD.IsMatch(chord) == true && chord.Contains("Dm") == false)
            {
                chord.Replace("D", "D#");
                return chord;
            }
            else if (regexG.IsMatch(chord) == true && chord.Contains("Gm") == false)
            {
                chord.Replace("G", "G#");
                return chord;
            }
            else if (regexAm.IsMatch(chord) == true)
            {
                chord.Replace("Am", "A#m");
                return chord;
            }
            else if (regexDm.IsMatch(chord) == true)
            {
                chord.Replace("Dm", "D#m");
                return chord;
            }
            else if (regexGm.IsMatch(chord) == true)
            {
                chord.Replace("Gm", "G#m");
                return chord;
            }
            else if (regexCm.IsMatch(chord) == true)
            {
                chord.Replace("Cm", "C#m");
                return chord;
            }
            else if (regexFm.IsMatch(chord) == true)
            {
                chord.Replace("Fm", "F#m");
                return chord;
            }
            else if (regexBbm.IsMatch(chord) == true)
            {
                chord.Replace("Bbm", "Bm");
                return chord;
            }
            else if (regexEbm.IsMatch(chord) == true)
            {
                chord.Replace("Ebm", "Em");
                return chord;
            }
            else if (regexAbm.IsMatch(chord) == true)
            {
                chord.Replace("Abm", "Am");
                return chord;
            }
            else if (regexDbm.IsMatch(chord) == true)
            {
                chord.Replace("Dbm", "Dm");
                return chord;
            }
            else if (regexGbm.IsMatch(chord) == true)
            {
                chord.Replace("Gbm", "Gm");
                return chord;
            }
            else if (regexBm.IsMatch(chord) == true)
            {
                chord.Replace("Bm", "Cm");
                return chord;
            }
            else if (regexEm.IsMatch(chord) == true)
            {
                chord.Replace("Em", "Fm");
                return chord;
            }
            else return "error";
        }

        public string Flat(string chord)
        {
            if (regexC.IsMatch(chord) == true && chord.Contains("Cm") == false)
            {
                chord.Replace("C", "B");
                return chord;
            }
            else if (regexF.IsMatch(chord) == true && chord.Contains("Fm") == false)
            {
                chord.Replace("F", "E");
                return chord;
            }
            else if (regexBb.IsMatch(chord) == true && chord.Contains("Bbm") == false && chord.Contains("A#m") == false)
            {
                chord.Replace("Bb", "A");
                chord.Replace("A#", "A");
                return chord;
            }
            else if (regexEb.IsMatch(chord) == true && chord.Contains("Ebm") == false && chord.Contains("D#m") == false)
            {
                chord.Replace("Eb", "D");
                chord.Replace("D#", "D");
                return chord;
            }
            else if (regexAb.IsMatch(chord) == true && chord.Contains("Abm") == false && chord.Contains("G#m") == false)
            {
                chord.Replace("Ab", "G");
                chord.Replace("G#", "G");
                return chord;
            }
            else if (regexDb.IsMatch(chord) == true && chord.Contains("Dbm") == false && chord.Contains("C#m") == false)
            {
                chord.Replace("Db", "C");
                chord.Replace("C#", "C");
                return chord;
            }
            else if (regexGb.IsMatch(chord) == true && chord.Contains("Gbm") == false && chord.Contains("F#m") == false)
            {
                chord.Replace("Gb", "F");
                chord.Replace("F#", "F");
                return chord;
            }
            else if (regexB.IsMatch(chord) == true && chord.Contains("Bm") == false)
            {
                chord.Replace("B", "Bb");
                return chord;
            }
            else if (regexE.IsMatch(chord) == true && chord.Contains("Em") == false)
            {
                chord.Replace("E", "Eb");
                return chord;
            }
            else if (regexA.IsMatch(chord) == true && chord.Contains("Am") == false)
            {
                chord.Replace("A", "Ab");
                return chord;
            }
            else if (regexD.IsMatch(chord) == true && chord.Contains("Dm") == false)
            {
                chord.Replace("D", "Db");
                return chord;
            }
            else if (regexG.IsMatch(chord) == true && chord.Contains("Gm") == false)
            {
                chord.Replace("G", "Gb");
                return chord;
            }
            else if (regexAm.IsMatch(chord) == true)
            {
                chord.Replace("Am", "Abm");
                return chord;
            }
            else if (regexDm.IsMatch(chord) == true)
            {
                chord.Replace("Dm", "Dbm");
                return chord;
            }
            else if (regexGm.IsMatch(chord) == true)
            {
                chord.Replace("Gm", "Gbm");
                return chord;
            }
            else if (regexCm.IsMatch(chord) == true)
            {
                chord.Replace("Cm", "Bm");
                return chord;
            }
            else if (regexFm.IsMatch(chord) == true)
            {
                chord.Replace("Fm", "Em");
                return chord;
            }
            else if (regexBbm.IsMatch(chord) == true)
            {
                chord.Replace("Bbm", "Am");
                return chord;
            }
            else if (regexEbm.IsMatch(chord) == true)
            {
                chord.Replace("Ebm", "Dm");
                return chord;
            }
            else if (regexAbm.IsMatch(chord) == true)
            {
                chord.Replace("Abm", "Gm");
                return chord;
            }
            else if (regexDbm.IsMatch(chord) == true)
            {
                chord.Replace("Dbm", "Cm");
                return chord;
            }
            else if (regexGbm.IsMatch(chord) == true)
            {
                chord.Replace("Gbm", "Fm");
                return chord;
            }
            else if (regexBm.IsMatch(chord) == true)
            {
                chord.Replace("Bm", "Bbm");
                return chord;
            }
            else if (regexEm.IsMatch(chord) == true)
            {
                chord.Replace("Em", "Ebm");
                return chord;
            }
            else return "error";
        }
    }
    
}
