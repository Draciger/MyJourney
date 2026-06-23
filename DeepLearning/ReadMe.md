# PGR207 – Deep Learning

Two separate deep learning projects: a mandatory assignment comparing CNN training configurations on MNIST, and a group exam project on medical image segmentation using the Kvasir-SEG dataset.

---

## Mandatory Assignment – CNN Training Configuration Study (MNIST)

A comparative study of nine CNN training experiments on the MNIST handwritten digit dataset, each isolating a single variable to measure its effect on model performance.

**Notebooks:** `MandatoryAssignmentV1.ipynb`, `MandatoryAssignmentV3.ipynb`

### Model Architecture
A lightweight TinyVGG-inspired CNN with two convolutional blocks (Conv → ReLU → MaxPool), followed by fully connected layers and a Softmax output layer for 10-digit classification. An MLP baseline is also implemented for reference.

### Experimental Variables
Nine independent runs were conducted, three for each category:

**Data Augmentation** — Random rotation (rotate10), random crop (randcrop), horizontal flip (hflip)

**Learning Rate Schedulers** — StepLR (step), CosineAnnealingLR (cosine), ReduceLROnPlateau (plateau)

**Optimizers** — SGD (sgd), RMSprop (rmsprop), Adam (adam)

All experiments used early stopping, the same base architecture, and ran for up to 10 epochs on CPU via Google Colab.

### Key Findings
- All runs achieved strong test accuracy between 98.56% and 99.49%
- Best overall: `rotate10` (99.49% accuracy, lowest test loss)
- Worst overall: `hflip` (98.56%) — horizontal flipping confuses similar-looking digits like 2 and 5
- Learning rate schedulers produced the lowest validation losses, indicating well-calibrated predictions
- RMSprop was the best-performing optimizer; all three schedulers performed similarly
- No strong correlation found between training time and model accuracy

### Evaluation Metrics
Test accuracy, test loss, and total training time per run.

---

## Exam – Medical Image Segmentation (Kvasir-SEG)

A group project (Candidates 2, 6, 17) investigating how different training factors affect the performance of a deep learning segmentation model on the Kvasir-SEG polyp dataset.

**Notebooks:** `Exam__resolution.ipynb`, `Exam_Data_Augmentation.ipynb`, `Exam_TrainDataSize.ipynb`

### Model Architecture
A U-Net segmentation model built with `segmentation_models_pytorch`, trained on image-mask pairs from the Kvasir-SEG dataset. Early stopping based on validation Dice score was used across all experiments.

### Task 1 – Impact of Image Resolution (`Exam__resolution.ipynb`)
Trains and evaluates the model across four input resolutions: 64×64, 128×128, 256×256, and 512×512. Investigates how image resolution affects segmentation quality and training efficiency.

### Task 2 – Impact of Data Augmentation (`Exam_Data_Augmentation.ipynb`)
Compares five augmentation strategies against a no-augmentation baseline:
- **Geometric** – horizontal and vertical flipping
- **Photometric** – brightness, contrast, and color adjustments
- **Noise** – Gaussian or similar noise injection
- **Combined** – all of the above applied together

### Task 3 – Impact of Training Data Size (`Exam_TrainDataSize.ipynb`)
Uses a fixed resolution of 256×256 and trains the model on 25%, 50%, and 75% of the training data to measure how dataset size affects segmentation performance.

### Evaluation Metrics
Dice score and IoU (Intersection over Union), tracked per epoch with early stopping to prevent overfitting.

---

## Libraries Used

- `torch`, `torchvision` – model building and training
- `segmentation_models_pytorch` – U-Net segmentation architecture
- `albumentations` – image augmentation pipelines
- `matplotlib`, `numpy`, `pandas` – visualization and data handling
- `PIL` – image loading and processing

---

## Dataset

| Dataset | Used in |
|---------|---------|
| MNIST (via `torchvision`) | Mandatory Assignment |
| Kvasir-SEG (polyp segmentation) | Exam |
