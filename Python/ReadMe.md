# PGR107 – Python Data Analysis

Three Jupyter Notebook tasks covering data exploration, data structures, and real-world price analysis using Python and Pandas. Each task builds on core data science skills including loading, cleaning, transforming and visualizing data.

---

## Tasks

### Task 1 – Exploratory Data Analysis
Loads and explores `data_PGR107.csv`, a restaurant dataset containing information about tips, total bills, group sizes, and days of the week.
- Initial exploration using `.describe()` for statistical summaries and `.info()` for data types and structure
- Histograms with KDE curves to visualize the distribution of tips and total bills
- Boxplots comparing tips, total bills, and group sizes across different weekdays
- Bar chart showing number of meals served per day
- Conclusions drawn from patterns found in the data, such as tipping behaviour and busiest days

### Task 2 – Pandas Series & DataFrames
Works with a list of Norwegian hospitals sourced from Wikipedia, building structured data from scratch using Pandas.
- Creates a detailed `Series` for Oslo University Hospital including name, location, campuses, employees, type, and founding year
- Builds 16 individual hospital `Series` covering hospitals across Norway
- Combines all Series into a single `DataFrame` for a structured overview
- Adds a `Regional Health Authority` column, assigning the correct health authority to each hospital based on its location (e.g. Southern and Eastern, Western, Central, or Northern Norway)

### Task 3 – Nord Pool Power Prices
A real-world use case analyzing electricity prices across Europe using data downloaded from Nord Pool — Europe's leading power market.
- Loads weekly price data from October 2022 and hourly price data from a day in November 2022
- Data cleaning and inspection to handle NaN values and verify data types
- Computes an average Norwegian price (`NOR`) based on six Norwegian cities: Oslo, Bergen, Kristiansand, Molde, Tromsø, and Trondheim
- Correlation heatmaps comparing price movements between European countries (Germany, France, Poland, Austria, Finland, Netherlands, Norway) and between Norwegian cities
- Bar charts and scatter plots to visualize and compare price changes across regions

---

## Libraries Used

- `pandas` – data loading, transformation, and structure
- `matplotlib` – plotting and figure layout
- `seaborn` – statistical visualizations including heatmaps, boxplots, and histograms

---

## Data Files Required

| File | Used in | Description |
|------|---------|-------------|
| `data_PGR107.csv` | Task 1 | Restaurant tips dataset |
| `weekly_prices_october_2022.csv` | Task 3 | Weekly Nord Pool prices, October 2022 |
| `hourly_prices_2022_11_23.csv` | Task 3 | Hourly Nord Pool prices, 23 Nov 2022 |
