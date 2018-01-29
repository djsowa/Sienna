--Dostępne produkty, które są przypisane do więcej niż jednej kategorii


--PostgreSQL based schema Query.

SELECT pr."Code", pr."Description", COUNT(pc) AS "CategoriesCount"
FROM
public."Products" AS pr
INNER JOIN public."ProductsToCategories" AS pc ON pc."ProductId" = pr."Id" 
INNER JOIN public."Categories" AS ca on ca."Id" = pc."CategoryId"
WHERE pr."IsAvailable" = true
GROUP BY pr."Code", pr."Description", pr."Id"
HAVING COUNT(pc) > 1
ORDER BY pr."Id"