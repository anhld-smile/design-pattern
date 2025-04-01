interface MonAnFactory
{
    HuTieu LayToHuTieu();
    My LayToMy();
}

interface HuTieu : ICloneable
{
    string GetModelDetails();
}

interface My : ICloneable
{
    string GetModelDetails();
}

abstract class MonAn : ICloneable
{
    public string Ten { get; set; }
    public string Topping { get; set; }

    public object Clone()
    {
        return this.MemberwiseClone();
    }

    public abstract string GetModelDetails();
}

class HuTieuNac : MonAn, HuTieu
{
    public override string GetModelDetails()
    {
        return $"Hu Tieu Nac - {Ten}, Topping: {Topping}";
    }
}

class HuTieuGio : MonAn, HuTieu
{
    public override string GetModelDetails()
    {
        return $"Hu Tieu Gio - {Ten}, Topping: {Topping}";
    }
}

class MyNac : MonAn, My
{
    public override string GetModelDetails()
    {
        return $"Mi Nac - {Ten}, Topping: {Topping}";
    }
}

class MyGio : MonAn, My
{
    public override string GetModelDetails()
    {
        return $"Mi Gio - {Ten}, Topping: {Topping}";
    }
}

class MonAnBuilder
{
    private MonAn monAn;

    public MonAnBuilder(MonAn monAn)
    {
        this.monAn = monAn;
    }

    public MonAnBuilder SetTen(string ten)
    {
        monAn.Ten = ten;
        return this;
    }

    public MonAnBuilder AddTopping(string topping)
    {
        monAn.Topping = topping;
        return this;
    }

    public MonAn Build()
    {
        return monAn;
    }
}

class LoaiGioFactory : MonAnFactory
{
    public HuTieu LayToHuTieu()
    {
        return new MonAnBuilder(new HuTieuGio()).SetTen("Hu Tieu Gio").AddTopping("Xuong ham").Build() as HuTieu;
    }

    public My LayToMy()
    {
        return new MonAnBuilder(new MyGio()).SetTen("Mi gio").AddTopping("Rau thom").Build() as My;
    }
}

class LoaiNacFactory : MonAnFactory
{
    public HuTieu LayToHuTieu()
    {
        return new MonAnBuilder(new HuTieuNac()).SetTen("Hu tieu nac").AddTopping("Thit nac").Build() as HuTieu;
    }

    public My LayToMy()
    {
        return new MonAnBuilder(new MyNac()).SetTen("Mi nac").AddTopping("Hanh phi").Build() as My;
    }
}

class NhaHang
{
    private static NhaHang _instance;
    private NhaHang() { }

    public static NhaHang GetInstance()
    {
        if (_instance == null)
        {
            _instance = new NhaHang();
        }
        return _instance;
    }

    public void HienThiMenu(Client client)
    {
        Console.WriteLine("********* HU TIEU **********");
        Console.WriteLine(client.GetHuTieuDetails());

        Console.WriteLine("******* MY **********");
        Console.WriteLine(client.GetMyDetails());
    }
}

class Client
{
    HuTieu hutieu;
    My my;

    public Client(MonAnFactory factory)
    {
        hutieu = factory.LayToHuTieu();
        my = factory.LayToMy();
    }

    public string GetHuTieuDetails()
    {
        return hutieu.GetModelDetails();
    }

    public string GetMyDetails()
    {
        return my.GetModelDetails();
    }

    public Client Clone()
    {
        return this.MemberwiseClone() as Client;
    }
}

class Program
{
    static void Main()
    {
        MonAnFactory loaiNac = new LoaiNacFactory();
        MonAnFactory loaiGio = new LoaiGioFactory();

        Client NacClient = new Client(loaiNac);
        Client GioClient = new Client(loaiGio);

        NhaHang nhaHang = NhaHang.GetInstance();

        nhaHang.HienThiMenu(NacClient);
        nhaHang.HienThiMenu(GioClient);

        Client clonedClient = GioClient.Clone();
        Console.WriteLine("------ CLONED CLIENT MENU ------");
        nhaHang.HienThiMenu(clonedClient);

        Console.ReadKey();
    }
}