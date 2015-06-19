// FUCK.cpp : Defines the entry point for the console application.
//


#include <stdafx.h>
#include <iostream>
using namespace std;

typedef unsigned long long ull;

int main()
{
	ull a = 13;
	ull c = 1;
	ull answer = 0;
	ull temp,temp1;
	while(a<1000000)
	{
	temp = a;
	c = 1;
		while(temp!=1)
		{
			if(temp==1)
			break;
			else if (temp%2==0){
			temp/=2;
			c++;}
			else{
			temp = (3*temp)+1;
			c++;}
		}
		if(c>answer){
		answer = c;
		temp1 = a;}
		a++;
	}
	cout << temp1 << endl;
}
