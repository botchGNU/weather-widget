-- SELECT * FROM citylist
-- where    rowid not in
--          (
--          select  min(rowid)
--          from    citylist
--          group by
--                  countryzip
--          ,       cityname
--          );
-- Maximale Temperatur:
SELECT MAX(maxtemperature) as maxtemp FROM weatherinfo WHERE weatherdaytime BETWEEN '2022-05-13 00:00:00' AND '2022-05-14 00:00:00';
-- Minimale Temperatur:
SELECT MIN(mintemperature) as mintemp FROM weatherinfo WHERE weatherdaytime BETWEEN '2022-05-13 00:00:00' AND '2022-05-14 00:00:00';
-- mittlere Temperatur:
SELECT
	(SUM(maxtemperature)+SUM(mintemperature))/
	(COUNT(maxtemperature)+COUNT(mintemperature)) as average FROM weatherinfo 
	WHERE weatherdaytime BETWEEN '2022-05-12 00:00:00' AND '2022-05-13 00:00:00';
	
-- Max, Min, Mittlere Temperatur
SELECT
	MAX(maxtemperature) as maxtemp,
	MIN(mintemperature) as mintemp ,
	(SUM(maxtemperature)+SUM(mintemperature))/
	(COUNT(maxtemperature)+COUNT(mintemperature)) as average
	FROM weatherinfo 
	WHERE weatherdaytime BETWEEN '2022-05-12 00:00:00' AND '2022-05-13 00:00:00';
	
-- Maximale Windgeschwindigkeit + Windrichtung
SELECT MAX(windspeed) as maxwind, winddirectionasstring as winddir FROM weatherinfo WHERE weatherdaytime BETWEEN '2022-05-13 00:00:00' AND '2022-05-14 00:00:00';
-- Minimale Windgeschwindigkeit + Windrichtung
SELECT MIN(windspeed) as minwind, winddirectionasstring as winddir FROM weatherinfo WHERE weatherdaytime BETWEEN '2022-05-13 00:00:00' AND '2022-05-14 00:00:00';
-- Häufigste Wetterart
SELECT weatherdescription, COUNT(weatherdescription) as frequency
	FROM weatherinfo 
	WHERE weatherdaytime BETWEEN '2022-05-15 00:00:00' AND '2022-05-15 12:00:00'
	GROUP BY weatherdescription
	LIMIT 1;
	
-- Häufigste Wetterarten (hier 2)
SELECT weatherdescription, COUNT(weatherdescription) AS frequency
	FROM weatherinfo
	WHERE weatherdaytime BETWEEN '2022-05-13 00:00:00' AND '2022-05-14 00:00:00'
	GROUP BY weatherdescription
	ORDER BY frequency DESC
	LIMIT 2;
	
SELECT cityid, cityname, countryzip FROM citylist GROUP BY cityid, cityname, countryzip;--LIKE "%" GROUP BY cityname;

SELECT * FROM citylist
where    rowid not in
          (
          select  min(rowid)
          from    citylist
          group by
                  countryzip
          ,       cityname
          );