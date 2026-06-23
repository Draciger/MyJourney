#PRG110 – Visual Analytics

---

## Overview

This project explores two datasets using **Tableau** to extract meaningful business and social insights through interactive dashboards and data visualizations.

- **Dataset 1** – Company X: customer, product, and financial data across multiple European countries
- **Dataset 2** – UN World Happiness Report: country happiness rankings and contributing factors over time

---

## Datasets

### Dataset 1 – Company X
| Type | Examples |
|------|----------|
| Categorical | Delivery mode, segment, city, country |
| Continuous | Sales, quantity, profit |

### Dataset 2 – World Happiness Report
| Type | Examples |
|------|----------|
| Categorical | Country, region |
| Continuous | Happiness rank, happiness score, freedom, generosity, economy, health |

---

## Dashboards & Key Insights

### Dashboard 1 – Orders by Country & Profitability
- **Insight 1:** Most orders originate from the UK and Germany, but on average profit, Norway ranks highest — showing strong potential in smaller markets.
- **Insight 2:** Sweden has the highest loss despite being third in sales volume. A deeper product-level investigation is recommended.

### Dashboard 2 – Sales, Quantity & Profit Over Time
- **Insight 3:** UK and Germany show steady growth across all metrics. Sweden continues to grow in sales and quantity while simultaneously losing money — a critical issue to address before other markets follow the same pattern. Norway shows positive growth across all three metrics.

### Dashboard 3 – Segment Sales Over Time
- **Insight 4:** All segments grew over time but dipped in the last quarter. The Consumer segment consistently leads. Home Office overtook Corporate in the final quarter — a potential emerging trend worth monitoring.

### Dashboard 4 – Sub-Category Sales & Profit
- **Insight 5:** Phones, copiers, and bookcases are the top three sub-categories by both sales and profit. Tables are the only sub-category generating a net loss.
- **Insight 6:** UK profits from table sales while Sweden and Germany do not — cross-country knowledge sharing is recommended to understand what drives the difference.

### Dashboard 5 – Regional Health & Family vs. Happiness
- **Insight 7:** Strong correlation observed between Family, Health (life expectancy), and Happiness Score across all regions. Regions scoring high in both Family and Health consistently rank higher in happiness.

### Dashboard 6 – Freedom & Happiness Score (2015 vs. 2022)
- **Insight 8:** Countries with significant gains in Freedom between 2015 and 2022 also show notable increases in Happiness Score. Countries with minimal freedom gains plateaued or declined in happiness.

### Dashboard 7 – Generosity vs. Economy (Scatterplot)
- **Insight 9:** Higher economic scores generally correlate with higher generosity. Exception: Sub-Saharan Africa scores highest in generosity despite a relatively weaker economy.

### Dashboard 8 – Top 10 Happiest & Unhappiest Countries
- **Insight 10:** The top 10 happiest countries are predominantly European (exceptions: Puerto Rico and Oman). The 10 unhappiest countries are located in Africa or Asia.

---

## Design Principles

The dashboard design followed core visual analytics principles to maximize clarity and minimize cognitive load:

- **Simplicity first** – Avoiding clutter, excessive decoration, and unnecessary axis labels (inspired by Stephen Few, *Information Dashboard Design*, 2006)
- **Single-screen layout** – All relevant information visible at once, no scrolling required
- **Purposeful color** – Color used sparingly and intentionally; accessibility for color-blind users considered (avoiding red/green-only distinctions)
- **Chart type rationale:**
  - **Bar charts** for categorical comparisons (sub-categories, regions, segments)
  - **Line charts** for trends and continuous data over time
  - **Scatterplots** for correlation analysis (generosity vs. economy)
- **Tooltips** added for on-demand detail without visual overload

---

## Tools Used

- [Tableau](https://www.tableau.com/) – Data visualization and dashboard creation

---

## References

- Few, Stephen. *Information Dashboard Design*. O'Reilly, 2006.
- Knaflic, Cole Nussbaumer. *Storytelling with Data*. John Wiley & Sons, 2015.
- iSixSigma. "Categorical vs. Continuous Data; What's the Difference?" 2022. https://www.isixsigma.com/methodology/categorical-vs-continuous-data-whats-the-difference/
