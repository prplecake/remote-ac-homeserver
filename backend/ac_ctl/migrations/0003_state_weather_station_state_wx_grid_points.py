# Generated by Django 4.2 on 2023-05-14 21:01

from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('ac_ctl', '0001_squashed_0002_dhtsensordata'),
    ]

    operations = [
        migrations.AddField(
            model_name='state',
            name='weather_station',
            field=models.TextField(blank=True),
        ),
        migrations.AddField(
            model_name='state',
            name='wx_grid_points',
            field=models.TextField(blank=True),
        ),
    ]
