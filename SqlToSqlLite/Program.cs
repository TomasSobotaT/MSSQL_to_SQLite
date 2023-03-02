using DataDownloader;
using System;

SpravceDB spravceDB = new SpravceDB();      // spravce databaze
bool databazeMSOk = false;                  // existují soubory databáze MSSQL
bool databazeLiteOk = false;                // existují soubory databáze SQLite     


Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("MS-SQL TO SQLITE 1.0");
Console.ResetColor();
Console.WriteLine();
Console.WriteLine("Program načte data ze souboru: Database.mdf a uloží je do Database.sqlite.");
Console.WriteLine("Funguje jen pro databáze s vytvořenými tabulkami pro projekt ITN Data Downloader.");
Console.WriteLine("Databáze je potřeba mít ve složce s programem - ve složce Database/");

Console.WriteLine(Directory.GetCurrentDirectory() + "\\Database\\Database.mdf");
//kontrola existence potřebných souborů souborů
if (File.Exists(Directory.GetCurrentDirectory() + "\\Database\\Database.mdf"))
{
   
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine("Databáze Database/Database.mdf - existuje");
    Console.ResetColor();
    databazeMSOk = true;
}
else
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Databáze Database/Database.mdf - neexistuje");
    Console.ResetColor();
}


if (File.Exists(Directory.GetCurrentDirectory() + "\\Database\\Database.sqlite"))
{
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine("Databáze Database/Database.sqlite - existuje");
    Console.ResetColor();
    databazeLiteOk = true;
}
else
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Databáze Database/Database.sqlite - neexistuje");
    Console.ResetColor();
}

// pokud databáze existují
if(databazeLiteOk && databazeMSOk)
{
    Console.WriteLine("Stisknutím jakékoliv klávesy zahájíte kopírování databáze.");
    Console.ReadKey();
    Console.WriteLine("Připojuji k MS-SQL databázi a vytahuji data.");
    try
    {
        spravceDB.ZiskejDataMSSql();    // načtení dat z MSSQL
    }
    catch (Exception ex)
    {
        Console.WriteLine("Nastala chyba při práci s MS-SQL databází: " + ex.Message);
        Console.ReadKey();
        Environment.Exit(-1);
    }
    Console.WriteLine("Načtení dat z MS-SQL databáze proběhlo v pořádku.");


    Console.WriteLine("Připojuji se k SQLite databázi a ukládám data");
    try
    {
        spravceDB.UlozDoSqlite();       //Uložení dat do SQLite
    }
    catch (Exception ex)
    {
        Console.WriteLine("Nastala chyba při práci s SQLite databází: " + ex.Message);
        Console.ReadKey();
        Environment.Exit(-1);
    }
    Console.WriteLine("Uložení dat do SQLite databáze proběhlo v pořádku.");

}

else
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Nakopírujte správné databázové soubory do složky Database a spusťte program znovu!");
    Console.ResetColor();
}

Console.ReadKey();

