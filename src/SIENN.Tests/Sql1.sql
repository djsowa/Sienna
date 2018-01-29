--Niedostępne produkty, których dostawa jest przewidywana w bieżącym miesiącu


--PostgreSQL based schema Query.

SELECT pr."Code", pr."Description", pr."Price", pr."IsAvailable", pr."NextDelivery"
FROM public."Products" AS pr
WHERE pr."IsAvailable" = false AND pr."NextDelivery" >= date_trunc('month', now() + interval '1 month') 
ORDER BY pr."Id"