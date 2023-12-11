# first test

# Встановлюємо необхідний пакет для роботи з графікою
install.packages("ggplot2")
library(ggplot2)

# Зчитуємо дані з CSV-файлу, вказуючи кому як роздільник
data <- read.csv("R_Test.csv", sep = ",")

# Відображення графіку
ggplot(data, aes(x = CH1, y = CH2)) +
  geom_line() +
  labs(title = "Графік даних з CSV-файлу", x = "CH1", y = "PD")
