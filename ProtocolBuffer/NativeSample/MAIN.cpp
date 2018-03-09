#include <iostream>
#include<fstream>
using namespace std;
#include "message.pb.h"
void main()
{
	MRSystem mrsystem;
	mrsystem.set_mnreceiverssupported(true);
	mrsystem.set_productname("Panorama");
	mrsystem.set_mainsystemtype(T15);
	mrsystem.set_nrofhreceivers(1);
	CompanyInfo* companyInfoptr = mrsystem.add_companyinfo();
	companyInfoptr->set_name("Philips");
	companyInfoptr->set_date("date1");

	std::fstream output("myfile", ios::out | ios::binary);
	mrsystem.SerializePartialToOstream(&output);

	output.close();
	
	MRSystem readMrSystem;

	std::fstream input("myfile", ios::in | ios::binary);
	readMrSystem.ParseFromIstream(&input);

	printf("%s\n", readMrSystem.productname().c_str());
}
