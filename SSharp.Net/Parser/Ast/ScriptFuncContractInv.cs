using Scripting.SSharp.Runtime;

namespace Scripting.SSharp.Parser.Ast
{
  /// <summary>
  /// 
  /// </summary>
  internal class ScriptFuncContractInv : ScriptExpr
  {
    private readonly ScriptExprList _list;

    public ScriptFuncContractInv(AstNodeArgs args)
      : base(args)
    {
      _list = ChildNodes[1] as ScriptExprList;
    }

    public override void Evaluate(IScriptContext context)
    {
      if (_list == null)
      {
        context.Result = true;
        return;
      }

      var result = true;     
      _list.Evaluate(context);
      var rez = (object[])context.Result;
      foreach (var o in rez)
      {
        try
        {
          result = result & (bool)o;
        }
        catch
        {
          throw new ScriptVerificationException(Strings.VerificationNonBoolean);
        }
      }

      context.Result = result;
    }
  }
}