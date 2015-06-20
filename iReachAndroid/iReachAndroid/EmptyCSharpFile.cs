﻿<?xml version="1.0" encoding="utf-8"?>
	<android.support.v4.widget.DrawerLayout xmlns:android="http://schemas.android.com/apk/res/android"
	xmlns:app="http://schemas.android.com/apk/res-auto"
	android:id="@+id/drawer_layout"
	android:layout_width="match_parent"
	android:layout_height="match_parent">
	<LinearLayout
	android:layout_width="match_parent"
	android:layout_height="match_parent"
	android:orientation="vertical">
	<include
	layout="@layout/toolbar"
	android:layout_width="match_parent"
	android:layout_height="wrap_content" />
	<FrameLayout
	android:id="@+id/main_content"
	android:layout_width="match_parent"
	android:layout_height="match_parent" />
	</LinearLayout>
	<android.support.design.widget.NavigationView
	android:id="@+id/nvView"
	android:layout_width="240dp"
	android:layout_height="match_parent"
	android:layout_gravity="start"
	android:background="@android:color/white" />
	</android.support.v4.widget.DrawerLayout>
