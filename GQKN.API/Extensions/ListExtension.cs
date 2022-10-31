using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace PVI.GQKN.API.Extensions;

public static class ListExtension
{
    public static List<T> AddRanges<T>(this List<T> list, IEnumerable<T> items)
    {
        list.AddRange(items);
        return list;
    }

    public static List<Claim> ToClaims(
        this IEnumerable<AclOperation> ops)
    {
        var claimsMap = new Dictionary<string, ulong>();
        foreach (var op in ops)
        {
            ulong claim;

            if (!claimsMap.TryGetValue(op.Scope, out claim))
            {
                claim = 0;
            }

            claimsMap[op.Scope] = claim | op.Id;
        }

        var claims = claimsMap.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()))
            .ToList();

        return claims;
    }

    public static List<Claim> ToClaims(
        this IEnumerable<QuyenDto> quyenDtos)
    {
        var ops = quyenDtos.Select(e => {
             var segs = AclOperation.Decode(e.Key);

             return new AclOperation(
                 order: 0,
                 id: segs.id,
                 scope: segs.nhom,
                 resource: e.Resource,
                 name: e.Name,
                 code: null
             );
         });

        return ops.ToClaims();
    }

    public static List<Claim> ToClaims(
        this IEnumerable<string> opIds)
    {
        var selectedOps = opIds.Select(e => {
            var segs = AclOperation.Decode(e);
            //  (Order, Resource, Id, Name, Scope)
            return new AclOperation(
                order: 0, 
                resource: null, 
                id: segs.id, 
                name: null, 
                scope: segs.nhom,
                code: null);

        }).Where(p => p != null);

        return selectedOps.ToClaims();
    }

    public static List<AclOperation> ToOps(this IEnumerable<Claim> claims, IEnumerable<AclOperation> acls)
    {
        List<AclOperation> ops = new List<AclOperation>();

        foreach (var claim in claims)
        {
            ops.AddRange(claim.ToOps(acls));
        }

        return ops;
    }

    public static List<ResourceGroupDto> ToResourceGroups(this IEnumerable<string> permissions, 
        IEnumerable<AclOperation> acls)
    {
        var userAcls = acls.Where(e => permissions.Contains(e.Key));
        var groups = (from u in userAcls
                      group u by u.Resource into g
                      select g);

        var dtoResources = (from u in groups
                            select new ResourceGroupDto()
                            {
                                ResourceId = u.Key,
                                QuyenIds = u.ToList().Select(e => e.Key).ToList()
                            }).ToList();

        return dtoResources;
    }
}
