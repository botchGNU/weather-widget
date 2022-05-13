-- following lines was used to create new table without duplicates
-- SELECT id, cityname, countryzip FROM citylist 
-- 	GROUP BY cityname 
-- 	ORDER BY cityname ASC;
-- 
-- -- create new citytable without duplicates
-- CREATE TABLE citylistNEW AS
-- 	SELECT id, cityname, countryzip FROM citylist 
-- 	GROUP BY cityname 
-- 	ORDER BY cityname ASC;
-- 
-- -- drop old citylist table	
-- DROP TABLE citylist;
-- 
-- -- create the new table as citylist
-- CREATE TABLE citylist AS
-- 	SELECT id, cityname, countryzip FROM citylistNEW 
-- 	GROUP BY cityname 
-- 	ORDER BY cityname ASC;
-- 	
-- --drop citylistNEW TABLE
-- DROP TABLE citylistNEW;


--LIKE "%" GROUP BY cityname;


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

-- following query demonstrates the query for search citynames
SELECT cityname FROM citylist 
	WHERE upper(cityname) LIKE 'RA%';
	
SELECT cityname FROM citylist