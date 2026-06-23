

# PGR210 - Machine Learning & Natural Language Processing

Five Jupyter Notebook tasks covering core ML and NLP techniques — from classification and clustering to regression, spam detection, and movie recommendations.

---

## Machine Learning Tasks

### ML Task 1 – Classification (Customer Health Profile)
Builds a Decision Tree classifier to predict health-related outcomes from customer data.
- Data preprocessing: removes duplicates, fills missing BMI values with the column mean
- Encodes categorical features: label encoding for `Smoking_Habit`, ordinal mapping for `Exercise_Frequency`
- Scales features using MinMaxScaler before training
- Trains a Decision Tree and evaluates accuracy across different `max_depth` values to find the optimal tree depth
- Visualizes results with a train/test accuracy plot and a normalized confusion matrix

**Dataset:** `ML_Task1_Customer_Health_Profile.csv`

### ML Task 2 – Clustering (Online Shopping Behaviour)
Groups online shoppers into distinct behavioural segments using K-Means clustering.
- Handles missing values in `Page_Views` using mean imputation via `SimpleImputer`
- Uses the Elbow Method (WCSS) to determine the optimal number of clusters — found to be 3
- Applies normalization and standardization across four different feature pairings: Purchase Amount vs Page Views, Purchase Amount vs Session Duration, Bounce Rate vs Page Views, and Session Duration vs Bounce Rate
- Visualizes all four clusterings in a 2×2 subplot grid, with cluster centroids highlighted

**Dataset:** `Online_Shopping_Behavior.csv`

### ML Task 3 – Regression (Rental Price Prediction)
Trains a neural network to predict monthly rental prices based on property features.
- Explores data using pairplots and a correlation heatmap to understand feature relationships
- Fills missing `Property_Area` values with the column mean
- Splits data into train (60%), validation (20%), and test (20%) sets
- Builds and trains a neural network using Keras, tracking MSE across epochs for both training and validation sets
- Evaluates the final model with a true vs. predicted scatter plot and a KDE distribution comparison

**Dataset:** `Rental_Price_Prediction.csv`

---

## NLP Tasks

### NLP Task 1 – Spam Detection
Classifies emails as spam or non-spam using topic modelling and Logistic Regression.
- Loads and explores a labelled email dataset, checking for null values and class distribution
- Visualizes spam vs. non-spam balance with a pie chart
- Preprocesses text: lowercasing, whitespace removal, punctuation/digit stripping, stopword removal, and stemming/lemmatization using NLTK
- Applies LDA (Latent Dirichlet Allocation) with TF features and SVD with TF-IDF features to reduce dimensionality
- Trains a Logistic Regression classifier on both representations and evaluates with accuracy, F1-score, and 5-fold cross-validation

**Dataset:** `PGR210_NLP_data1.csv`

### NLP Task 2 – Movie Recommendation System
Builds a content-based movie recommender using TF-IDF and cosine similarity on movie metadata.
- Loads a movie dataset with semicolon delimiter; drops the `homepage` column due to excessive nulls; fills missing taglines with empty strings
- Focuses on descriptive text fields: `genres`, `keywords`, `overview`, and `tagline`
- Vectorizes the combined text using TF-IDF
- Computes cosine similarity between Spider-Man and all other movies to find the top 5 most similar films
- Applies Truncated SVD (10 components) to the TF-IDF matrix and repeats the similarity search to compare results between TF-IDF and SVD-based recommendations

**Dataset:** `PGR210_NLP_data2.csv`

---

## Libraries Used

- `pandas`, `numpy` – data handling and transformation
- `matplotlib`, `seaborn` – visualization
- `scikit-learn` – preprocessing, clustering, classification, dimensionality reduction, and evaluation
- `nltk` – text preprocessing (tokenization, stopwords, stemming, lemmatization)
- `keras` / `tensorflow` – neural network for regression

---

## Data Files Required

| File | Used in |
|------|---------|
| `ML_Task1_Customer_Health_Profile.csv` | ML Task 1 |
| `Online_Shopping_Behavior.csv` | ML Task 2 |
| `Rental_Price_Prediction.csv` | ML Task 3 |
| `PGR210_NLP_data1.csv` | NLP Task 1 |
| `PGR210_NLP_data2.csv` | NLP Task 2 |
