# from rest_framework import generics
# from django.shortcuts import render
# from .models import Book
# from .serializers import BookSerializer
#from fastapi import FastAPI
from django.http import JsonResponse
import pandas as pd

def my_api(request):
    tasker_df = pd.read_csv("tasker_df.csv")
    # Droping unnecossory features
    new_tasker_df = tasker_df[
        ['UserId', 'Email', 'UserName', 'FirstName', 'LastName', 'TotalReviews', 'AvgReviews', 'Skills']]
    # Split the skills column by comma
    Skills = new_tasker_df["Skills"].str.split(",", expand=True)

    # Give the columns meaningful names
    Skills.columns = [f"Skill_{i + 1}" for i in range(Skills.shape[1])]

    # Concatenate the original DataFrame with the new skills DataFrame
    new_tasker_df_exp = pd.concat([new_tasker_df, Skills], axis=1)

    # Drop the original skills column
    new_tasker_df_exp = new_tasker_df_exp.drop("Skills", axis=1)
    new_tasker_df_exp[['Skill_1', 'Skill_2', 'Skill_3', 'Skill_4', 'Skill_5', 'Skill_6', 'Skill_7']] = \
    new_tasker_df_exp[['Skill_1', 'Skill_2', 'Skill_3', 'Skill_4', 'Skill_5', 'Skill_6', 'Skill_7']].fillna('')
    v = new_tasker_df_exp['TotalReviews']
    R = new_tasker_df_exp['AvgReviews']
    C = new_tasker_df_exp['AvgReviews'].mean()
    m = new_tasker_df_exp['TotalReviews'] >= 5
    new_tasker_df_exp['weighted_average'] = ((R * v) + (C * m)) / (v + m)

    weighted_average_result_df = new_tasker_df_exp[new_tasker_df_exp['TotalReviews'] >= 20].sort_values(
        'weighted_average', ascending=False)
    # Create a new column with first and last name concatenated
    weighted_average_result_df['FullName'] = weighted_average_result_df.apply(
        lambda x: x['FirstName'] + ' ' + x['LastName'], axis=1)

    top_taskers = weighted_average_result_df[
        ['UserId', 'FullName', 'TotalReviews', 'AvgReviews', 'Skill_1', 'Skill_2', 'Skill_3', 'Skill_4', 'Skill_5',
         'Skill_6', 'Skill_7']].head(10)

    result = []
    for index, row in top_taskers.iterrows():
        user_details = {}
        user_details['UserId'] = row['UserId']
        user_details['FullName'] = row['FullName']
        user_details['TotalReview'] = row['TotalReviews']
        user_details['AvgReview'] = row['AvgReviews']
        #user_details['PhoneOne'] = row['PhoneOne']
        skills = [skill for skill in
                  row[['Skill_1', 'Skill_2', 'Skill_3', 'Skill_4', 'Skill_5', 'Skill_6', 'Skill_7']].tolist() if skill]
        user_details['Skills'] = skills
        result.append(user_details)

    return JsonResponse(result, safe=False)

def recommended_users(request, skill):
    # Load the tasker_df.csv file into a Pandas DataFrame
    tasker_df = pd.read_csv("tasker_df.csv")

    # Select only the relevant columns from the DataFrame
    new_tasker_df = tasker_df[
        ['UserId', 'Email', 'UserName', 'FirstName', 'LastName', 'TotalReviews', 'AvgReviews','PhoneOne', 'Skills']]

    # Split the skills into separate columns
    Skills = new_tasker_df["Skills"].str.split(",", expand=True)

    # Give the columns meaningful names
    Skills.columns = [f"Skill_{i + 1}" for i in range(Skills.shape[1])]

    # Concatenate the original DataFrame with the new skills DataFrame
    new_tasker_df_exp = pd.concat([new_tasker_df, Skills], axis=1)

    # Drop the original skills column
    new_tasker_df_exp = new_tasker_df_exp.drop("Skills", axis=1)

    # Fill the NaN values with empty strings
    new_tasker_df_exp[['Skill_1', 'Skill_2', 'Skill_3', 'Skill_4', 'Skill_5', 'Skill_6', 'Skill_7']] = \
    new_tasker_df_exp[['Skill_1', 'Skill_2', 'Skill_3', 'Skill_4', 'Skill_5', 'Skill_6', 'Skill_7']].fillna('')

    # Calculate the weighted average
    v = new_tasker_df_exp['TotalReviews']
    R = new_tasker_df_exp['AvgReviews']
    C = new_tasker_df_exp['AvgReviews'].mean()
    m = new_tasker_df_exp['TotalReviews'] >= 5
    new_tasker_df_exp['weighted_average'] = ((R * v) + (C * m)) / (v + m)

    # Sort the DataFrame based on the weighted average
    weighted_average_result_df = new_tasker_df_exp[new_tasker_df_exp['TotalReviews'] >= 20].sort_values(
        'weighted_average', ascending=False)

    # Definig function for skill base recommendation
    def skill_rec(skill):
        # Convert the values in all skill columns to lowercase
        for skill_col in ['Skill_1', 'Skill_2', 'Skill_3', 'Skill_4', 'Skill_5', 'Skill_6', 'Skill_7']:
            weighted_average_result_df[skill_col] = weighted_average_result_df[skill_col].str.lower()

        # Create a list of the skill columns
        skill_cols = ['Skill_1', 'Skill_2', 'Skill_3', 'Skill_4', 'Skill_5', 'Skill_6', 'Skill_7']

        # Create a sub-DataFrame that only contains rows where the specified skill is present in any of the skill columns
        skillBase = weighted_average_result_df[
            weighted_average_result_df[skill_cols].apply(lambda x: skill.lower() in x.values, axis=1)]

        # Sort the sub-DataFrame in descending order based on the weighted_average column
        skillBase = skillBase.sort_values(by='weighted_average', ascending=False)

        # Create a dictionary for each user containing their details
        user_dict_list = []
        for index, row in skillBase.iterrows():
            user_dict = {}
            user_dict["FullName"] = row["FirstName"] + " " + row["LastName"]
            user_dict["TotalReview"] = row["TotalReviews"]
            user_dict["AvgReview"] = row["AvgReviews"]
            user_dict["PhoneOne"] = row["PhoneOne"]
            user_dict["Skills"] = [skill for skill in row[skill_cols].values if skill]
            user_dict_list.append(user_dict)

        # Return the top 5 users with their details in dictionary form
        if not skillBase.empty:
            return user_dict_list[:5]
        else:
            print('No Tasker Available for this skill')

    # Call the skill_rec function to get the top 5 taskers with the specified skill
    result = skill_rec(skill)
    return JsonResponse(result, safe=False)
