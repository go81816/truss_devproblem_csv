# Truss CSV normalization problem

## Pre-requisites

* Internet connection (for downloading Docker images)
* Docker  
```
docker build -t gopland_truss/truss_deveval_csvnormalization -f Dockerfile .
type sample.csv | docker run -i -d gopland_truss/truss_deveval_csvnormalization
``` 

## Additional Questions

### Missing functionality:

* The FooDuration and BarDuration columns are in HH:MM:SS.MS format (where MS is milliseconds); please convert them to the total number of seconds.
* The TotalDuration column is filled with garbage data. For each row, please replace the value of TotalDuration with the sum of FooDuration and BarDuration.
* Invalid characters can be replaced with the Unicode Replacement Character. If that replacement makes data invalid (for example, because it turns a date field into something unparseable), print a warning to stderr and drop the row from your output.

### Talking points:

* sources of invalid encoding data experience
* alpine v ubuntu v windows timezone info