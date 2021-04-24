using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoration
{
	private string decoName;
	private string codeName;
	private int quantity;
	private int price;
	public string DecoName { get { return decoName; } }
	public int Price { get { return price; } }
	public int Quantity { get { return quantity; } }
	public string CodeName { get { return codeName; } }

	public Decoration(string decoN, string codN, int q, int p)
	{
		decoName = decoN;
		codeName = codN;
		quantity = q;
		price = p;
	}
}
