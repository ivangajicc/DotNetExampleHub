using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.AbstractFactory;

public interface IWheelFactory
{
    ISteelWheel CreateSteelWheel();

    IAlloyWheel CreateAlloyWheel();
}

public class AftermarketWheelFactory : IWheelFactory
{
    public IAlloyWheel CreateAlloyWheel() => new AftermarketAlloyWheel();

    public ISteelWheel CreateSteelWheel() => new AftermarketSteelWheel();
}

public class OemWheelFactory : IWheelFactory
{
    public IAlloyWheel CreateAlloyWheel() => new OemAlloyWheel();

    public ISteelWheel CreateSteelWheel() => new OemSteelWheel();
}
