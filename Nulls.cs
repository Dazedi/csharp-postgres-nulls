using System;
using System.Collections.Generic;
using System.Linq;	

					
public class Program
{
	public static void Main()
	{
		// Operator (depends on whether the data is ordered by asc or desc)
		string op = ">";

		// In case of Npgsql we create parameters that match the paramList strings
		List<string> paramList = new List<string>();
		paramList.Add(":x");
		paramList.Add(":y");
		paramList.Add(":z");
		
		// fieldList contains all of the fields used in orderBy (and therefore also in where)
		List<string> fieldList = new List<string>();
		fieldList.Add("a");
		fieldList.Add("b");
		fieldList.Add("c");

		// When switching to next page with keyset paging, you need the last row of current page. 
		// Previous page needs the first row of current page(?)
		// blocks contains bits (true/false) to see if the value is real NULL or not. In this case
		// "N" means null and "V" means value. 	
		List<string> blocks = new List<string>();
		blocks.Add("N");
		blocks.Add("N");
		blocks.Add("N");

		Console.WriteLine("DESC NULLS FIRST");
		PrintSQL("NN", op, new List<string>{"N","N"}, new List<string>{"a","b"}, new List<string>{":x",":y"}, 1);
		PrintSQL("NV", op, new List<string>{"N","V"}, new List<string>{"a","b"}, new List<string>{":x",":y"}, 1);
		PrintSQL("VN", op, new List<string>{"V","N"}, new List<string>{"a","b"}, new List<string>{":x",":y"}, 1);
		PrintSQL("VV", op, new List<string>{"V","V"}, new List<string>{"a","b"}, new List<string>{":x",":y"}, 1);
		PrintSQL("NNN", op, new List<string>{"N","N","N"}, fieldList, paramList, 1);
		PrintSQL("NNV", op, new List<string>{"N","N","V"}, fieldList, paramList, 1);
		PrintSQL("NVN", op, new List<string>{"N","V","N"}, fieldList, paramList, 1);
		PrintSQL("NVV", op, new List<string>{"N","V","V"}, fieldList, paramList, 1);
		PrintSQL("VNN", op, new List<string>{"V","N","N"}, fieldList, paramList, 1);
		PrintSQL("VNV", op, new List<string>{"V","N","V"}, fieldList, paramList, 1);
		PrintSQL("VVN", op, new List<string>{"V","V","N"}, fieldList, paramList, 1);
		PrintSQL("VVV", op, new List<string>{"V","V","V"}, fieldList, paramList, 1);
		paramList.Add(":w");
		fieldList.Add("d");

		PrintSQL("NNNN", op, new List<string>{"N","N","N","N"}, fieldList, paramList, 1);
		PrintSQL("NNNV", op, new List<string>{"N","N","N","V"}, fieldList, paramList, 1);
		PrintSQL("NNVN", op, new List<string>{"N","N","V","N"}, fieldList, paramList, 1);
		PrintSQL("NNVV", op, new List<string>{"N","N","V","V"}, fieldList, paramList, 1);
		PrintSQL("NVNN", op, new List<string>{"N","V","N","N"}, fieldList, paramList, 1);
		PrintSQL("NVNV", op, new List<string>{"N","V","N","V"}, fieldList, paramList, 1);
		PrintSQL("NVVN", op, new List<string>{"N","V","V","N"}, fieldList, paramList, 1);
		PrintSQL("NVVV", op, new List<string>{"N","V","V","V"}, fieldList, paramList, 1);
		PrintSQL("VNNN", op, new List<string>{"V","N","N","N"}, fieldList, paramList, 1);
		PrintSQL("VNNV", op, new List<string>{"V","N","N","V"}, fieldList, paramList, 1);
		PrintSQL("VNVN", op, new List<string>{"V","N","V","N"}, fieldList, paramList, 1);
		PrintSQL("VNVV", op, new List<string>{"V","N","V","V"}, fieldList, paramList, 1);
		PrintSQL("VVNN", op, new List<string>{"V","V","N","N"}, fieldList, paramList, 1);
		PrintSQL("VVNV", op, new List<string>{"V","V","N","V"}, fieldList, paramList, 1);
		PrintSQL("VVVN", op, new List<string>{"V","V","V","N"}, fieldList, paramList, 1);
		PrintSQL("VVVV", op, new List<string>{"V","V","V","V"}, fieldList, paramList, 1);

		Console.WriteLine("=============================================================================================");
		Console.WriteLine("ASC NULLS LAST");

		paramList.Remove(":w");
		fieldList.Remove("d");

		PrintSQL("NN", op, new List<string>{"N","N"}, new List<string>{"a","b"}, new List<string>{":x",":y"}, 1, false);
		PrintSQL("NV", op, new List<string>{"N","V"}, new List<string>{"a","b"}, new List<string>{":x",":y"}, 1, false);
		PrintSQL("VN", op, new List<string>{"V","N"}, new List<string>{"a","b"}, new List<string>{":x",":y"}, 1, false);
		PrintSQL("VV", op, new List<string>{"V","V"}, new List<string>{"a","b"}, new List<string>{":x",":y"}, 1, false);
		PrintSQL("NNN", op, new List<string>{"N","N","N"}, fieldList, paramList, 1, false);
		PrintSQL("NNV", op, new List<string>{"N","N","V"}, fieldList, paramList, 1, false);
		PrintSQL("NVN", op, new List<string>{"N","V","N"}, fieldList, paramList, 1, false);
		PrintSQL("NVV", op, new List<string>{"N","V","V"}, fieldList, paramList, 1, false);
		PrintSQL("VNN", op, new List<string>{"V","N","N"}, fieldList, paramList, 1, false);
		PrintSQL("VNV", op, new List<string>{"V","N","V"}, fieldList, paramList, 1, false);
		PrintSQL("VVN", op, new List<string>{"V","V","N"}, fieldList, paramList, 1, false);
		PrintSQL("VVV", op, new List<string>{"V","V","V"}, fieldList, paramList, 1, false);

		paramList.Add(":w");
		fieldList.Add("d");

		PrintSQL("NNNN", op, new List<string>{"N","N","N","N"}, fieldList, paramList, 1, false);
		PrintSQL("NNNV", op, new List<string>{"N","N","N","V"}, fieldList, paramList, 1, false);
		PrintSQL("NNVN", op, new List<string>{"N","N","V","N"}, fieldList, paramList, 1, false);
		PrintSQL("NNVV", op, new List<string>{"N","N","V","V"}, fieldList, paramList, 1, false);
		PrintSQL("NVNN", op, new List<string>{"N","V","N","N"}, fieldList, paramList, 1, false);
		PrintSQL("NVNV", op, new List<string>{"N","V","N","V"}, fieldList, paramList, 1, false);
		PrintSQL("NVVN", op, new List<string>{"N","V","V","N"}, fieldList, paramList, 1, false);
		PrintSQL("NVVV", op, new List<string>{"N","V","V","V"}, fieldList, paramList, 1, false);
		PrintSQL("VNNN", op, new List<string>{"V","N","N","N"}, fieldList, paramList, 1, false);
		PrintSQL("VNNV", op, new List<string>{"V","N","N","V"}, fieldList, paramList, 1, false);
		PrintSQL("VNVN", op, new List<string>{"V","N","V","N"}, fieldList, paramList, 1, false);
		PrintSQL("VNVV", op, new List<string>{"V","N","V","V"}, fieldList, paramList, 1, false);
		PrintSQL("VVNN", op, new List<string>{"V","V","N","N"}, fieldList, paramList, 1, false);
		PrintSQL("VVNV", op, new List<string>{"V","V","N","V"}, fieldList, paramList, 1, false);
		PrintSQL("VVVN", op, new List<string>{"V","V","V","N"}, fieldList, paramList, 1, false);
		PrintSQL("VVVV", op, new List<string>{"V","V","V","V"}, fieldList, paramList, 1, false);
		
		//Console.WriteLine(where);
	}
	
	public static void PrintSQL(string comment, string op, List<string> pattern, List<string> fieldList, List<string> paramList, int i, bool awayFromNull = true)
	{
		string groupBy = String.Join(",", fieldList);
		string orderBy = op == ">" ? String.Join(",", fieldList) : String.Join(" DESC,", fieldList) + " DESC";
		
		Console.WriteLine("-- " + comment);
		Console.WriteLine(String.Format("select {0} from test2 where {1} group by {2} order by {3}", groupBy, awayFromNull ? AwayFromNullWhere(op, pattern, fieldList, paramList, 1) : TowardsNullWhere(op, pattern, fieldList, paramList, 1), groupBy, orderBy));
	}
	
	public static string AwayFromNullWhere(string op, List<string> blocks, List<string> fieldList, List<string> paramList, int i)
	{
		if( i == blocks.Count ) // last
		{
			return "";
		}
		
		string joinBlock = "";
		
		if(i == 1)
		{
			joinBlock = blocks[i - 1] + blocks[i];
		}
		else 
		{
			joinBlock = blocks[i-2] + blocks[i-1] + blocks[i];
		}
				
		if(joinBlock == "NN")
		{
			return String.Format(" ( {0} is not null or ({0} is null and ({1} is not null ", fieldList[i-1], fieldList[i]) + AwayFromNullWhere(op, blocks, fieldList, paramList, i+1) + " ) ) )";
		}
		else if(joinBlock == "NV")
		{
			return String.Format(" ( {0} is not null or ( {0} is null and ( {1}{3}{2} ", fieldList[i-1], fieldList[i], paramList[i], op) + AwayFromNullWhere(op, blocks, fieldList, paramList, i+1) + " ) ) )";
		}
		else if(joinBlock == "VN")
		{
			return String.Format(" ( {0}{3}{1} or ( {0}<={1} and ( {2} is not null ", fieldList[i-1], paramList[i-1], fieldList[i], op) + AwayFromNullWhere(op, blocks, fieldList, paramList, i+1) + " ) ) )";
		}
		else if(joinBlock == "VV")
		{
			return String.Format(" ( {0}{4}{1} or ( {0}={1} and ( {2}{4}{3} ", fieldList[i-1], paramList[i-1], fieldList[i], paramList[i], op) + AwayFromNullWhere(op, blocks, fieldList, paramList, i+1) + " ) ) )";
		}
		else if(joinBlock == "NNN")
		{
			return String.Format(" or ( {0} is null and ( {1} is not null", fieldList[i-1], fieldList[i]) + AwayFromNullWhere(op, blocks, fieldList, paramList, i+1) + " ) )";
		}
		else if(joinBlock == "NNV")
		{
			return String.Format(" or ( {0} is null and ( {1}{3}{2}", fieldList[i - 1], fieldList[i], paramList[i], op) + AwayFromNullWhere(op, blocks, fieldList, paramList, i + 1) + " ) )";
		}
		else if(joinBlock == "NVN")
		{
			return String.Format(" or ( {0}{3}={1} and ( {2} is not null", fieldList[i - 1], paramList[i-1], fieldList[i], op) + AwayFromNullWhere(op, blocks, fieldList, paramList, i + 1) + " ) )";
		}
		else if(joinBlock == "NVV")
		{
			return String.Format(" or ( {0}={1} and ( {2}{4}{3}", fieldList[i - 1], paramList[i-1], fieldList[i], paramList[i], op) + AwayFromNullWhere(op, blocks, fieldList, paramList, i + 1) + " ) )";
		}
		else if(joinBlock == "VNN")
		{
			return String.Format(" or ( {0} is null and ( {1} is not null", fieldList[i - 1], fieldList[i]) + AwayFromNullWhere(op, blocks, fieldList, paramList, i + 1) + " ) )";
		}
		else if(joinBlock == "VNV")
		{
			return String.Format(" or ( {0} is null and ( {1}{3}{2}", fieldList[i-1], fieldList[i], paramList[i], op) + AwayFromNullWhere(op, blocks, fieldList, paramList, i+1) + " ) )";
		}
		else if(joinBlock == "VVN")
		{
			return String.Format(" or ( {0}{3}={1} and ({2} is not null", fieldList[i-1], paramList[i-1], fieldList[i], op) + AwayFromNullWhere(op, blocks, fieldList, paramList, i+1) + " ) )";
		}
		else if(joinBlock == "VVV")
		{
			return String.Format(" or ( {0}={1} and ( {2}{4}{3}", fieldList[i-1], paramList[i-1], fieldList[i], paramList[i], op) + AwayFromNullWhere(op, blocks, fieldList, paramList, i+1) + " ) )";
		}
		return "";
	}
	
	public static string TowardsNullWhere(string op, List<string> blocks, List<string> fieldList, List<string> paramList, int i, bool isValueSet = false, int nullLength = 0)
	{
		if( i == blocks.Count ) // last
		{
			if(!isValueSet)
			{
				return " null ";
			}
			return "";
		}
		
		string joinBlock = "";
		
		if(i == 1)
		{
			joinBlock = blocks[i - 1] + blocks[i];
		}
		else 
		{
			joinBlock = blocks[i-2] + blocks[i-1] + blocks[i];
		}
				
		if(joinBlock == "NN")
		{
			return TowardsNullWhere(op, blocks, fieldList, paramList, i+1, false, 2);
		}
		else if(joinBlock == "NV")
		{
			return String.Format(" {0} is null and ( {1}{3}{2} or {1} is null ", fieldList[i-1], fieldList[i], paramList[i], op) + TowardsNullWhere(op, blocks, fieldList, paramList, i+1, true, 0) + " )";
		}
		else if(joinBlock == "VN")
		{
			return String.Format(" {0}{2}{1} or {0} is null", fieldList[i-1], paramList[i-1], op) + TowardsNullWhere(op, blocks, fieldList, paramList, i+1, true, 1);
		}
		else if(joinBlock == "VV")
		{
			return String.Format(" {0}{4}{1} or {0} is null or ( {0}{4}={1} and ( {2}{4}{3} or {2} is null", fieldList[i-1], paramList[i-1], fieldList[i], paramList[i], op) + TowardsNullWhere(op, blocks, fieldList, paramList, i+1, true, 0) + " ) )";
		}
		else if(joinBlock == "NNN")
		{
			return TowardsNullWhere(op, blocks, fieldList, paramList, i+1, isValueSet, nullLength+1);
		}
		else if(joinBlock == "NNV")
		{
			List<string> nulls = new List<string>();
			for(int j = nullLength; j > 0; j--)
			{
				nulls.Add(fieldList[i-j] + " is null");
			}
			if(isValueSet)
			{
				return String.Format(" or ( {0}{5}={1} and {2} and ( {3}{5}{4} or {3} is null", fieldList[i-nullLength-1], paramList[i-nullLength-1], String.Join(" and ", nulls), fieldList[i], paramList[i], op) + TowardsNullWhere(op, blocks, fieldList, paramList, i+1, true, 0) + " ) )";
			}
			else
			{
				return String.Format(" {0} and ( {1}{3}{2} or {1} is null", String.Join(" and ", nulls), fieldList[i], paramList[i], op) + TowardsNullWhere(op, blocks, fieldList, paramList, i+1, true, 0) + " )";
			}
		}
		else if(joinBlock == "NVN")
		{
			return TowardsNullWhere(op, blocks, fieldList, paramList, i+1, true, 1);
		}
		else if(joinBlock == "NVV")
		{
			return String.Format(" or ( {0}{4}={1} and ( {2}{4}{3} or {2} is null", fieldList[i-1], paramList[i-1], fieldList[i], paramList[i], op) + TowardsNullWhere(op, blocks, fieldList, paramList, i+1, true, 0) + " ) )";
		}
		else if(joinBlock == "VNN")
		{
			return TowardsNullWhere(op, blocks, fieldList, paramList, i+1, true, nullLength+1);
		}
		else if(joinBlock == "VNV")
		{
			return String.Format(" or ( {0}{5}={1} and {2} is null and ( {3}{5}{4} or {3} is null", fieldList[i-2], paramList[i-2], fieldList[i-1], fieldList[i], paramList[i], op) + TowardsNullWhere(op, blocks, fieldList, paramList, i+1, true, 0) + " ) )";
		}
		else if(joinBlock == "VVN")
		{
			return TowardsNullWhere(op, blocks, fieldList, paramList, i+1, true, nullLength+1);
		}
		else if(joinBlock == "VVV")
		{
			return String.Format(" or ( {0}{4}={1} and ( {2}{4}{3} or {2} is null", fieldList[i-1], paramList[i-1], fieldList[i], paramList[i], op) + TowardsNullWhere(op, blocks, fieldList, paramList, i+1, true, 0) + " ) )";
		}
		
		return "";
	}
}