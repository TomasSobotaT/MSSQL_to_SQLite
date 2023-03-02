namespace DataDownloader
{ 
    /// <summary>
    /// Třída reprezentující uživatele ItNetwork.cz
    /// </summary>
    public class User
    {
        //Nick uživatele
        public string Jmeno { get; private set; }       
        //Obrazek jeho avatara

        public string ObrazekWWW { get; private set; }

        public string Stranka{ get; private set; }

        // Číslo/Id uživatele na Itnetwork.cz
        public int Id { get; private set; }

        // zkušenost uživatele (reprezentuje zkušenost získanou z absolvovaných testů, příkladů a lekcí)
        public int Zkusenost { get; private set; }

        //věk uživatele, pokud ho má nastaven
        public int Vek{ get; private set; }

        //aura uživatele (Aura okolo člena se vypočítává na základě toho jak hodnotí ostatní. Kdo často chválí má pozitivní auru, kdo jen kritizuje má auru negativní.)
        public int Aura { get; private set; }



        public User(string jmeno, string obrazekWWW, string stranka, int id, int vek, int zkusenost, int aura)
        {
            Jmeno = jmeno;
            ObrazekWWW = obrazekWWW;
            Stranka = stranka;
            Id = id;
            Zkusenost = zkusenost;
            Aura = aura;
            Vek = vek;
        }
    }
}
