using System;

namespace Domain.Entity;

public class Contact
{
    public int id {set; get;}
    public required string name {set; get;}
    public required string email {set; get;} 
    public required string phonenumber {set; get;}
}
