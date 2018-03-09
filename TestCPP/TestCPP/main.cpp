#include <iostream>
#include <vector>
using namespace std;


class A 
{
public:
	int dum;
	A():v(0)
	{
		cout<<"In the constructor of "<<i;
		//v.resize(10);
		i++;
	}

	A(const A& a)
	{
		cout<<"In the copy constructor of "<<i;
	}

	void check()
	{
		for ( int i = 0; i< 10; i++)
		{
			if( v[0] == NULL)
			{
				cout<<"Vector "<<i<<" is empty";
			}
		}
	}

	~A()
	{
		cout<<"In the destructor";
	}

private:
	static int i;
	vector<int*> v;

};

int A::i = 1;

enum class AB :int
{
	bla = 1, blu = 33
};

void main()
{
	{
		A a;
		A a1;
	cout<<"\n111111111111\n";
	{

		vector<A> arr;
		arr.resize(2);
		std::cout<<"\nAfter resize 0\n";
		a.dum = 11;
		arr[0] = a;
		std::cout<<"\nAfter pushin 1\n";
		a1.dum = 12;
		arr[1] = a1;
		std::cout<<"\nAfter pushin 2\n";
		std::cout<<"\nCapacity-"<<arr.capacity();
		arr.clear();
	}
	cout<<"\nzzzzzzzzz\n";

	//cout<<arr.size()<<std::endl;

//	arr.resize(0);
//	arr.clear();
	//cout<<arr.size();
	}
	cout<<"\nxxxxxxxx\n";

	/*int i;
	cin>>i;*/
}


//void main()
//{
//	const int i = 10;
//	int* p[i] = {NULL};
//	int** q = p;
//	cout<<"Hello";
//	A a;
//	a.check();
//}
//
