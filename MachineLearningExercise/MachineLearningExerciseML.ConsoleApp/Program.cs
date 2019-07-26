//*****************************************************************************************
//*                                                                                       *
//* This is an auto-generated file by Microsoft ML.NET CLI (Command-Line Interface) tool. *
//*                                                                                       *
//*****************************************************************************************

using System;
using System.IO;
using System.Linq;
using Microsoft.ML;
using System.Collections.Generic;
using MachineLearningExerciseML.ConsoleApp.DataModels;
using Microsoft.ML.Trainers.FastTree;

namespace MachineLearningExerciseML.ConsoleApp
{
    class Program
    {
        //Machine Learning model to load and use for predictions

        private static string TRAIN_DATA_FILEPATH = @"C:\Users\Navn\Desktop\carsbg.csv";
        private static string MODEL_FILEPATH = @"C:\Users\Navn\Desktop\ML\MachineLearningExercise\MachineLearningExerciseML.Model\MLModel.zip";


        static void Main(string[] args)
        {            
            if(!File.Exists(path: MODEL_FILEPATH))
            {
                TrainModel();
            }

            var listOfInputs = new List<ModelInput>()
            {
                new ModelInput
                {
                    Make="VW",
                    Model="Golf",
                    CubicCapacity=1400,
                    FuelType="Бензин",
                    Gear = "ръчни",
                    HorsePower = 55,
                    Range = 270000,
                    Year = "01/01/1992"
                },

                new ModelInput
                {
                    Make="Opel",
                    Model = "Zafira",
                    CubicCapacity = 1800,
                    FuelType="Бензин",
                    Gear="Ръчни",
                    HorsePower=115,
                    Range=180000,
                    Year="01/01/1999"
                },
            };

            TestModel(MODEL_FILEPATH,listOfInputs);
        }

        private static void TrainModel()
        {
            MLContext mlContext = new MLContext(seed: 1);

            // Load Data
            IDataView trainingDataView = mlContext.Data.LoadFromTextFile<ModelInput>(
                                            path: TRAIN_DATA_FILEPATH,
                                            hasHeader: true,
                                            separatorChar: ',',
                                            allowQuoting: true,
                                            allowSparse: false);

            // Build training pipeline
            var dataProcessPipeline = mlContext.Transforms.Categorical.OneHotEncoding(new[]
            {
                new InputOutputColumnPair("Make", "Make"),
                new InputOutputColumnPair("FuelType", "FuelType"),
                new InputOutputColumnPair("Year", "Year"),
                new InputOutputColumnPair("Gear", "Gear")
            })
                                      .Append(mlContext.Transforms.Categorical.OneHotHashEncoding(new[]
                                      {
                                          new InputOutputColumnPair("Model", "Model")
                                      }))
                                      .Append(mlContext.Transforms.Concatenate("Features", new[]
                                      {
                                          "Make", "FuelType", "Year", "Gear", "Model", "HorsePower", "Range", "CubicCapacity"
                                      }));

            // Set the training algorithm 
            var trainer = mlContext.Regression.Trainers.FastTreeTweedie(new FastTreeTweedieTrainer.Options()
            {
                NumberOfLeaves = 79,
                MinimumExampleCountPerLeaf = 10,
                NumberOfTrees = 500,
                LearningRate = 0.101134f,
                Shrinkage = 2.667976f,
                LabelColumnName = "Price",
                FeatureColumnName = "Features"
            });

            var trainingPipeline = dataProcessPipeline.Append(trainer);



            // Train Model
            ITransformer mlModel = trainingPipeline.Fit(trainingDataView);

            // Save model
            mlContext.Model.Save(mlModel, trainingDataView.Schema, MODEL_FILEPATH);
        }

        private static void TestModel(string MODEL_FILEPATH, List<ModelInput> listOfInputs)
        {
            var mlContext = new MLContext();
            var model = mlContext.Model.Load(MODEL_FILEPATH, out _);
            var predictionEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(model);

            foreach(var item in listOfInputs)
            {
                var predicts = predictionEngine.Predict(item);
                Console.WriteLine($"Make: {item.Model}");
                Console.WriteLine($"Make: {item.Make}");
                Console.WriteLine($"Make: {item.HorsePower}");
                Console.WriteLine($"Make: {item.Year}");
                Console.WriteLine($"Make: {item.Range}");

                Console.WriteLine($"Price: {predicts.Score}");
            }
        }
    }
}
