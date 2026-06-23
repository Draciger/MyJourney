# PGR304 – Energy Consumption Forecasting

A Jupyter Notebook project analyzing and forecasting electricity load using time series models. The dataset contains hourly observations of energy load alongside weather and market variables, and the goal is to predict future power consumption using VAR and ARIMA models.

---

## Tasks

### Task 1 – Understanding the Data
Loads and explores `PGR304 final_selected_load_predictors.csv`, a time series dataset with a timestamp index.
- Inspects data structure and statistical summaries using `.describe()` and `.info()`
- Checks for missing values (NaN and blank) across all columns
- Boxplots for each variable to identify outliers and spread
- Histograms to visualize distributions across the dataset
- Time series plots for all five variables to observe trends and seasonality over time

**Variables:** `load_MW`, `temp_C`, `price_DA_EUR_MWh`, `solar_MW`, `clouds_pct`

### Task 2 – Utility Value & Stationarity
Explores relationships between variables and tests whether the data is stationary — a requirement for time series modeling.
- Correlation heatmap to identify which predictors relate most strongly to energy load
- VAR (Vector Autoregression) model fitted to all variables to examine multivariate relationships
- Stationarity testing using both ADF (Augmented Dickey-Fuller) and KPSS tests
- First-order differencing applied to variables that fail the stationarity tests, followed by re-testing

### Task 3 – Forecasting with VAR & ARIMA
Builds and evaluates two forecasting models on a 48-hour test set.

**VAR Model:**
- Trained on differenced data, then converted back to original scale for interpretable forecasts
- Evaluated using MSE, RMSE, MAE, and MAPE
- Forecast plotted against actual load values

**ARIMA Model:**
- Grid search over `p` and `q` parameters (0–3) with `d=1` to find the best model by AIC
- Forecasts 48 hours ahead using the best-fit order
- Evaluated using the same metrics as VAR

**Model Comparison:**
- Side-by-side metric comparison (MAE, RMSE, MAPE) between VAR and ARIMA
- Combined plot showing actual load vs. both forecasts over the 48-hour test window

---

## Libraries Used

- `pandas` – data loading and time series handling
- `numpy` – numerical operations
- `matplotlib` – plotting and visualization
- `seaborn` – heatmaps and statistical plots
- `statsmodels` – VAR model, ARIMA model, ADF and KPSS stationarity tests
- `sklearn` – MAE and MSE evaluation metrics

---

## Data File Required

| File | Description |
|------|-------------|
| `PGR304 final_selected_load_predictors.csv` | Hourly energy load and weather/market predictors with timestamp index |
