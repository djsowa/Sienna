--Top 3 kategorie wraz z informacją o liczbie przypisanych, dostępnych produktów oraz średnią
--ceną produktów w kategorii (top 3 powinno pokazywać kategorie, których średnia cen
--produktów jest największa)


--PostgreSQL based schema Query.

SELECT ca."Code", ca."Description", COUNT(pc) AS "NumberOFProducts", AVG(pr."Price") AS "AveragePrice" FROM 
public."Categories" AS ca
INNER JOIN public."ProductsToCategories" AS pc ON pc."CategoryId" = ca."Id" 
INNER JOIN public."Products" AS pr on pr."Id" = pc."ProductId"
WHERE pr."IsAvailable" = true 
GROUP BY ca."Code", ca."Description"
ORDER BY AVG(pr."Price") DESC
LIMIT 3