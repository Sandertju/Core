// <auto-generated />
namespace Resgrid.Repositories.DataRepository.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    
    public sealed partial class ChangedDepartmentGroupNameToString : IMigrationMetadata
    {
        string IMigrationMetadata.Id
        {
            get { return "201208012319131_ChangedDepartmentGroupNameToString"; }
        }
        
        string IMigrationMetadata.Source
        {
            get { return null; }
        }
        
        string IMigrationMetadata.Target
        {
            get { return "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"; }
        }
    }
}