using UnityEngine;
using System.Collections;

public class CubicInterp  {
	//control points

	public int divide;
	public Vector3[] p;
	
	float[] x;
	float[] y;
	float[] z;
	float[] xExt;
	float[] yExt;
	float[] zExt;
	private Vector3[] outV;
	float [][] xyPara ;
	float [][] xzPara ;
	float[] h;
	int n;
	
	// Use this for initialization

	
	public CubicInterp(Vector3[] inputP,int d)
	{
		p=inputP;
		n=p.Length;
		divide=d;
		x=new float[n];
		y=new float[n];
		z=new float[n];
		h=new float[n-1];
		xyPara=new float[4][];
		xzPara=new float[4][];
	}

	public Vector3[] CubicCalc()
	{
		if(n==0)Debug.Log("no data in input vectors");
		for(int i=0;i<n;i++)
		{
			x[i]=p[i].x;
			y[i]=p[i].y;
			z[i]=p[i].z;
		}
		for(int i=0;i<n-1;i++)
		{
			h[i]=x[i+1]-x[i];
		}
		xyPara=Cinterp(x,y);
		xzPara=Cinterp(x,z);
		xExt=Xinterp();
		yExt=Finterp(xyPara);
		zExt=Finterp(xzPara);
		Vector3[] outV=new Vector3[xExt.Length];
		for(int i=0;i<xExt.Length-1;i++){
			outV[i]=new Vector3(xExt[i],yExt[i],zExt[i]);
		}
		outV[xExt.Length-1]=new Vector3(x[x.Length-1],y[x.Length-1],z[x.Length-1]);
		return outV;
	}
	

	private float[] TDMA(float [] ta,float [] tb,float [] tc, float [] tx)
	{
		int n=tx.Length;
		tc[0]=tc[0]/tb[0];
		tx[0]=tx[0]/tb[0];

		for(int i=1;i<n;i++){
			float m=1/(tb[i]-ta[i]*tc[i-1]);
			tc[i]=tc[i]*m;
			tx[i]=(tx[i]-ta[i]*tx[i-1])*m;
		}
		for(int i=n-2;i>0;i--)
		{
			tx[i]=tx[i]-tc[i]*tx[i+1];
		}
		return tx;
	}
	
	private float[][] Cinterp(float[] x, float[] y)
	{
		float[] m=new float[n];
		float[][] result=new float[][]{new float[n],new float[n],new float[n],new float[n]};
		result[0][0]=0;
		result[1][0]=1;
		result[2][0]=0;
		result[3][0]=0;
		result[0][n-1]=0;
		result[1][n-1]=1;
		result[2][n-1]=0;
		result[3][n-1]=0;
		for(int i=1;i<n-1;i++)
		{
			result[0][i]=h[i-1];
			result[1][i]=2*(h[i-1]+h[i]);
			result[2][i]=h[i];
			result[3][i]=6*((y[i+1]-y[i])/h[i]-(y[i]-y[i-1])/h[i-1]);
		}
		m=TDMA(result[0],result[1],result[2],result[3]);
		for(int i=0;i<n-1;i++)
		{
			result[0][i]=y[i];
			result[1][i]=(y[i+1]-y[i])/h[i]-h[i]*m[i]/2-h[i]*(m[i+1]-m[i])/6;
			result[2][i]=m[i]/2;
			result[3][i]=(m[i+1]-m[i])/(6*h[i]);
		}
		return result;
	}
	private float[] Xinterp()
	{
		float[] xExt=new float[(x.Length-1)*divide+1];
		Debug.Log(xExt.Length);
		int i=0;
		for(;i<(x.Length-1);i++)
		{
			for(int j=0;j<divide;j++)
			{
			xExt[i*divide+j]=x[i]+h[i]/divide*j;
			}
		}	
		xExt[i*divide]=x[i];
		return xExt;
	}
	private float[] Finterp(float[][] p)
	{
		float[] yout=new float[xExt.Length];
		int i=0;
		for(;i<xExt.Length;i++)
		{
			int seg=(int)i/divide;
			float hDist=xExt[i]-xExt[seg*divide];
			yout[i]=p[0][seg]+p[1][seg]*hDist+p[2][seg]*hDist*hDist+p[3][seg]*hDist*hDist*hDist;
		}
		yout[xExt.Length-1]=y[y.Length-1];
		return yout;
	}
}

