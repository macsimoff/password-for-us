using PasswordForUsLibrary.Import.StringParser;

namespace TestsForPasswordForUs.ParsingTest;

public class HomeFileStringParserTest
{
    private readonly HomeFileStringParser _stringParser = new();

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void CreateNodeData_Test01()
    {
        var insertData = "elbaadmin"+HomeFileStringParser.Delimiter
                               +"maksimov.o@skbkontur.ru"+HomeFileStringParser.Delimiter
                               +"WhatDoesTheFoxSay?";
        var res = this._stringParser.CreateNodeData(insertData);

        Assert.Multiple(() =>
        {
            Assert.That( res.Title, Is.EqualTo("elbaadmin"));
            Assert.That(res.Login, Is.EqualTo("maksimov.o@skbkontur.ru"));
            Assert.That(res.Password, Is.EqualTo("WhatDoesTheFoxSay?"));
        });
    }

    [Test]
    public void CreateNodeData_Test02()
    {
        var insertData = "postgreSQL"+HomeFileStringParser.Delimiter
                                    +"pr0st0Pa$$";
        var res = this._stringParser.CreateNodeData(insertData);

        Assert.Multiple(() =>
        {
            Assert.That( res.Title, Is.EqualTo("postgreSQL"));
            Assert.That(res.Password, Is.EqualTo("pr0st0Pa$$"));
        });
    }

    [Test]
    public void CreateNodeData_Test03()
    {
        var insertData = "Аэроклуб боевой SQLсервер"+HomeFileStringParser.Delimiter
                                    +"mow1db5.aeroclub.int"+HomeFileStringParser.Delimiter
                                    +"oleg.maksimov"+HomeFileStringParser.Delimiter
                                    +"Zgvn@9R2h";
        var res = this._stringParser.CreateNodeData(insertData);

        Assert.Multiple(() =>
        {
            Assert.That( res.Title, Is.EqualTo("Аэроклуб боевой SQLсервер"));
            Assert.That( res.Url, Is.EqualTo("mow1db5.aeroclub.int"));
            Assert.That( res.Login, Is.EqualTo("oleg.maksimov"));
            Assert.That(res.Password, Is.EqualTo("Zgvn@9R2h"));
        });
    }

    [Test]
    public void CreateNodeData_Test04()
    {
        var insertData = "https://app.otta.com/"+HomeFileStringParser.Delimiter
                               +"noleg.macsimoff@gmail.com"+HomeFileStringParser.Delimiter
                               +"1a$tpa$$4";
        var res = this._stringParser.CreateNodeData(insertData);
        Assert.Multiple(() =>
        {
            Assert.That( res.Title, Is.EqualTo("https://app.otta.com/"));
            Assert.That( res.Url, Is.EqualTo("https://app.otta.com/"));
            Assert.That( res.Login, Is.EqualTo("noleg.macsimoff@gmail.com"));
            Assert.That(res.Password, Is.EqualTo("1a$tpa$$4"));
        });
    }

    [Test]
    public void CreateNodeData_Test05()
    {
        var insertData = "Url"+HomeFileStringParser.TagSeparator+" https://www.reference-my-tenant.com/bin/login.pl?app=1477794" + HomeFileStringParser.Delimiter +
                         "Login"+HomeFileStringParser.TagSeparator+"oleg.macsimoff@gmail.com" + HomeFileStringParser.Delimiter +
                         "Password"+HomeFileStringParser.TagSeparator+"m8ezef5g";
        var res = this._stringParser.CreateNodeData(insertData);
        Assert.Multiple(() =>
        {
            Assert.That( res.Title, Is.EqualTo("https://www.reference-my-tenant.com/bin/login.pl?app=1477794"));
            Assert.That( res.Url, Is.EqualTo("https://www.reference-my-tenant.com/bin/login.pl?app=1477794"));
            Assert.That( res.Login, Is.EqualTo("oleg.macsimoff@gmail.com"));
            Assert.That(res.Password, Is.EqualTo("m8ezef5g"));
            Assert.That(res.Data.Count, Is.EqualTo(0));
        });
    }
    [Test]
    public void CreateNodeData_Test06()
    {
        var insertData = "Name"+HomeFileStringParser.TagSeparator+" name" + HomeFileStringParser.Delimiter +
                         "Url"+HomeFileStringParser.TagSeparator+"https://www.reference-my-tenant.com/bin/login.pl?app=1477794" + HomeFileStringParser.Delimiter +
                         "Login"+HomeFileStringParser.TagSeparator+"oleg.macsimoff@gmail.com" + HomeFileStringParser.Delimiter +
                         "Password"+HomeFileStringParser.TagSeparator+"m8ezef5g";
        var res = this._stringParser.CreateNodeData(insertData);
        Assert.Multiple(() =>
        {
            Assert.That( res.Title, Is.EqualTo("name"));
            Assert.That( res.Url, Is.EqualTo("https://www.reference-my-tenant.com/bin/login.pl?app=1477794"));
            Assert.That( res.Login, Is.EqualTo("oleg.macsimoff@gmail.com"));
            Assert.That(res.Password, Is.EqualTo("m8ezef5g"));
        });
    }
    [Test]
    public void CreateNodeData_Test07()
    {
        var insertData = "PassUser"+HomeFileStringParser.TagSeparator+" Anna" + HomeFileStringParser.Delimiter +
                         "National Insurance Number (NINO)" + HomeFileStringParser.Delimiter +
                         "data"+HomeFileStringParser.TagSeparator+" SW912292A";
        var res = this._stringParser.CreateNodeData(insertData);
        Assert.Multiple(() =>
        {
            Assert.That( res.User, Is.EqualTo("Anna"));
            Assert.That( res.Title, Is.EqualTo("National Insurance Number (NINO)"));
            Assert.That(res.Data["data"], Is.EqualTo("SW912292A"));
        });
    }
    [Test]
    public void CreateNodeData_Test08()
    {
        var insertData = "PassUser"+HomeFileStringParser.TagSeparator+" Anna" + HomeFileStringParser.Delimiter +
                         "Government Gateway user ID" + HomeFileStringParser.Delimiter +
                         "https://www.tax.service.gov.uk/self-assessment" + HomeFileStringParser.Delimiter +
                         "ann.macsimoff@gmail.com" + HomeFileStringParser.Delimiter +
                         "m4p#R$0na11d" + HomeFileStringParser.Delimiter +
                         "Government Gateway user ID"+HomeFileStringParser.TagSeparator+" 42 40 63 95 40 57";
        var res = this._stringParser.CreateNodeData(insertData);
        Assert.Multiple(() =>
        {
            Assert.That( res.User, Is.EqualTo("Anna"));
            Assert.That( res.Title, Is.EqualTo("Government Gateway user ID"));
            Assert.That( res.Url, Is.EqualTo("https://www.tax.service.gov.uk/self-assessment"));
            Assert.That( res.Login, Is.EqualTo("ann.macsimoff@gmail.com"));
            Assert.That(res.Password, Is.EqualTo("m4p#R$0na11d"));
            Assert.That(res.Data["Government Gateway user ID"], Is.EqualTo("42 40 63 95 40 57"));
        });
    }
    
    [Test]
    public void CreateNodeData_Test09()
    {
        var insertData = "PassUser"+HomeFileStringParser.TagSeparator+" Anna" + HomeFileStringParser.Delimiter +
                         "https://kino.pub/" + HomeFileStringParser.Delimiter +
                         "ann.macsimoff@gmail.com" + HomeFileStringParser.Delimiter +
                         "AnnBgg131!";
        var res = this._stringParser.CreateNodeData(insertData);
        Assert.Multiple(() =>
        {
            Assert.That( res.User, Is.EqualTo("Anna"));
            Assert.That( res.Title, Is.EqualTo("https://kino.pub/"));
            Assert.That( res.Url, Is.EqualTo("https://kino.pub/"));
            Assert.That( res.Login, Is.EqualTo("ann.macsimoff@gmail.com"));
            Assert.That(res.Password, Is.EqualTo("AnnBgg131!"));
            Assert.That(res.Data.Count(), Is.EqualTo(0));
        });
    }
    
    [Test]
    public void CreateNodeData_Test10()
    {
        var insertData = "Wigmore Hall" + HomeFileStringParser.Delimiter +
                         "https://wigmore-hall.org.uk/" + HomeFileStringParser.Delimiter +
                         "ann.macsimoff@gmail.com" + HomeFileStringParser.Delimiter +
                         "AnnBgg123!" + HomeFileStringParser.Delimiter +
                         "кодовое слово для доступа к данным"+HomeFileStringParser.TagSeparator+" pikaPika";
        var res = this._stringParser.CreateNodeData(insertData);
        Assert.Multiple(() =>
        {
            Assert.That( res.Title, Is.EqualTo("Wigmore Hall"));
            Assert.That( res.Url, Is.EqualTo("https://wigmore-hall.org.uk/"));
            Assert.That( res.Login, Is.EqualTo("ann.macsimoff@gmail.com"));
            Assert.That(res.Password, Is.EqualTo("AnnBgg123!"));
            Assert.That(res.Data["кодовое слово для доступа к данным"], Is.EqualTo("pikaPika"));
        });
    }
    
    [Test]
    public void CreateNodeData_Test11()
    {
        var insertData = "revolut" + HomeFileStringParser.Delimiter +
                         "pin"+HomeFileStringParser.TagSeparator+" 5612" + HomeFileStringParser.Delimiter +
                         "pass"+HomeFileStringParser.TagSeparator+" 8985";
        var res = this._stringParser.CreateNodeData(insertData);
        Assert.Multiple(() =>
        {
            Assert.That( res.Title, Is.EqualTo("revolut"));
            Assert.That(res.Data["pin"], Is.EqualTo("5612"));
            Assert.That(res.Data["pass"], Is.EqualTo("8985"));
        });
    }
    
    [Test]
    public void CreateNodeData_Test12()
    {
        var insertData = "https://zp.midpass.ru/" + HomeFileStringParser.Delimiter +
                         "кодовое слово"+HomeFileStringParser.TagSeparator+" git4$ip";
        var res = this._stringParser.CreateNodeData(insertData);
        Assert.Multiple(() =>
        {
            Assert.That( res.Title, Is.EqualTo("https://zp.midpass.ru/"));
            Assert.That( res.Url, Is.EqualTo("https://zp.midpass.ru/"));
            Assert.That(res.Data["кодовое слово"], Is.EqualTo("git4$ip"));
        });
    }
    
    [Test]
    public void CreateNodeData_Test13()
    {
        var insertData = "cigna" + HomeFileStringParser.Delimiter +
                         "https://memberportal.cigna.co.uk/public/login.html" + HomeFileStringParser.Delimiter +
                         "ID"+HomeFileStringParser.TagSeparator+" CI70583023" + HomeFileStringParser.Delimiter +
                         "ann.macsimoff@gmail.com" + HomeFileStringParser.Delimiter +
                         "CiSvat38raT" + HomeFileStringParser.Delimiter +
                         "fist pet"+HomeFileStringParser.TagSeparator+" Pesha" + HomeFileStringParser.Delimiter +
                         "chaildhood hero"+HomeFileStringParser.TagSeparator+" Jacques-Yves Cousteau" + HomeFileStringParser.Delimiter +
                         "move or song"+HomeFileStringParser.TagSeparator+" Belleville";
        var res = this._stringParser.CreateNodeData(insertData);
        Assert.Multiple(() =>
        {
            Assert.That( res.Title, Is.EqualTo("cigna"));
            Assert.That( res.Url, Is.EqualTo("https://memberportal.cigna.co.uk/public/login.html"));
            Assert.That( res.Login, Is.EqualTo("ann.macsimoff@gmail.com"));
            Assert.That(res.Password, Is.EqualTo("CiSvat38raT"));
            Assert.That(res.Data["ID"], Is.EqualTo("CI70583023"));
            Assert.That(res.Data["fist pet"], Is.EqualTo("Pesha"));
            Assert.That(res.Data["chaildhood hero"], Is.EqualTo("Jacques-Yves Cousteau"));
            Assert.That(res.Data["move or song"], Is.EqualTo("Belleville"));
        });
        
    }
}