using System;
using System.Collections.Generic;
using System.Text;
using EmpManagement.Model;

namespace EmpManagement.ViewModel
{
    class EmpDetail: BaseViewModel

    {

    public Employee employee { get; set; }

    public EmpDetail(Employee emp)

    {

        employee = emp;

    }

    }

}