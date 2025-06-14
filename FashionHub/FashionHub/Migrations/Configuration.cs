namespace FashionHub.Migrations
{
  using System;
  using System.Collections.Generic;
  using System.Data.Entity;
  using System.Data.Entity.Migrations;
  using System.Data.Entity.Validation;
  using System.Linq;
  using System.Windows;
  using FashionHub.Models;

  internal sealed class Configuration : DbMigrationsConfiguration<FashionHub.Models.DataBaseContext>
  {
    public Configuration()
    {
      AutomaticMigrationsEnabled = false;
    }

    protected override void Seed(FashionHub.Models.DataBaseContext context)
    {

    }
  }
}


