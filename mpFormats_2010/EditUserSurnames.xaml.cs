﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;
using ModPlusAPI;
using ModPlusAPI.Windows;
using ModPlusAPI.Windows.Helpers;

namespace mpFormats
{
    /// <summary>
    /// Логика взаимодействия для MpStampFields.xaml
    /// </summary>
    public partial class EditUserSurnames
    {
        // Список стандартных должностей
        readonly List<string> _defvalues = new List<string>
        {
            "Утвердил","Составил","Изм. внес","Проверил","Нач. отд.","ГАП",
            "ГИП","Н.контр.","Т.контр.","Разраб.","Вед.инж.","Гл.констр."
        };

        private XElement _surnamesXml;
        public EditUserSurnames()
        {

            InitializeComponent();
            this.OnWindowStartUp();
        }
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        // Загрузка формы
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _surnamesXml = new XElement("UserSurnames");
                var confifDoc = UserConfigFile.ConfigFileXml;
                if (confifDoc.Element("Settings") != null)
                {
                    var sEl = confifDoc.Element("Settings");
                    // Если уже есть должности, то берем их
                    if (sEl?.Element("UserSurnames") != null)
                    {
                        if (sEl.Element("UserSurnames").Elements("Surname").Any())
                            foreach (var usn in sEl.Element("UserSurnames").Elements("Surname"))
                            {
                                _surnamesXml.Add(usn);
                            }
                        else
                        {
                            // Если нет ни одной записи, то создаем пустую
                            var newSurname = new XElement("Surname");
                            newSurname.SetAttributeValue("Id", "SnId1");
                            newSurname.SetAttributeValue("Surname", string.Empty);
                            _surnamesXml.Add(newSurname);
                        }
                    }
                    else
                    {
                        // Если нет ни одной записи, то создаем пустую
                        var newSurname = new XElement("Surname");
                        newSurname.SetAttributeValue("Id", "SnId1");
                        newSurname.SetAttributeValue("Surname", string.Empty);
                        _surnamesXml.Add(newSurname);
                    }
                }
                else
                {
                    ModPlusAPI.Windows.MessageBox.Show("Файл конфигурации поврежден!" + Environment.NewLine +
                          "Запустите Конфигуратор ModPlus", MessageBoxIcon.Close);
                    Close();
                }
                // Биндим
                DgSurnames.ItemsSource = _surnamesXml.Elements("Surname");
            }
            catch (Exception ex)
            {
                ExceptionBox.Show(ex);
            }
        }
        // Добавить строчку в список
        private void BtAddSurname_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_surnamesXml.Elements("Surname").Any())
                {
                    // Получаем список всех цифр
                    var ides =
                        _surnamesXml.Elements("Surname")
                            .Select(sn => int.Parse(sn.Attribute("Id").Value.Substring(4)))
                            .ToList();
                    var newId = 0;
                    while (true)
                    {
                        newId = ides.Last() + 1;
                        if (!ides.Contains(newId))
                            break;
                    }
                    if (newId != 0)
                    {
                        var newSurname = new XElement("Surname");
                        newSurname.SetAttributeValue("Id", "SnId" + newId);
                        newSurname.SetAttributeValue("Surname", string.Empty);
                        _surnamesXml.Add(newSurname);
                        // Refresh
                        RefreshDg();
                    }
                    else
                    {
                        ModPlusAPI.Windows.MessageBox.Show("Произошла ошибка!", MessageBoxIcon.Close);
                    }
                }
                else
                {
                    // Если нет ни одной записи, то создаем пустую
                    var newSurname = new XElement("Surname");
                    newSurname.SetAttributeValue("Id", "SnId1");
                    newSurname.SetAttributeValue("Surname", string.Empty);
                    _surnamesXml.Add(newSurname);

                    RefreshDg();
                }
            }
            catch (Exception ex)
            {
                ExceptionBox.Show(ex);
            }
        }

        private void RefreshDg()
        {
            DgSurnames.ItemsSource = null;
            DgSurnames.ItemsSource = _surnamesXml.Elements("Surname");
        }
        // Сохранение в файл настроек
        private void SaveToConfig()
        {
            try
            {
                // Открываем как xml
                var configFile = UserConfigFile.ConfigFileXml;
                // Проверяем есть ли группа Settings
                // Если нет, то сообщаем об ошибке
                if (configFile.Element("Settings") == null)
                {
                    ModPlusAPI.Windows.MessageBox.Show("Файл конфигурации поврежден!" + Environment.NewLine +
                          "Запустите Конфигуратор ModPlus", MessageBoxIcon.Close);
                    Close();
                }
                var element = configFile.Element("Settings");
                // Если есть элемент UserSurnames, то удаляем его!
                if (element?.Element("UserSurnames") != null)
                    element.Element("UserSurnames")?.Remove();
                // Добавляем текущий
                element?.Add(_surnamesXml);
                // Сохраняем
                UserConfigFile.SaveConfigFile();
            }
            catch (Exception ex)
            {
                ExceptionBox.Show(ex);
            }
        }
        // Удалить выбранную строчку
        private void BtRemoveSurname_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DgSurnames.SelectedIndex != -1)
                {
                    var selectedItem = DgSurnames.SelectedItem as XElement;
                    foreach (var sn in _surnamesXml.Elements("Surname"))
                    {
                        if (sn.Attribute("Id").Value.Equals(selectedItem.Attribute("Id").Value))
                        {
                            sn.Remove();
                            RefreshDg();
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionBox.Show(ex);
            }
        }
        // Закрытие окна
        private void EditUserSurnames_OnClosing(object sender, CancelEventArgs e)
        {
            try
            {
                // Проверяем, что не содержится стандартных значений
                foreach (var sn in _surnamesXml.Elements("Surname"))
                {
                    if (_defvalues.Contains(sn.Attribute("Surname")?.Value))
                    {
                        ModPlusAPI.Windows.MessageBox.Show("Значение " + sn.Attribute("Surname")?.Value +
                                      " уже содержится в списке стандартных значений", MessageBoxIcon.Alert);
                        e.Cancel = true;
                        return;
                    }
                }
                // Проверяем на наличие одинаковых значенйи
                var list = _surnamesXml.Elements("Surname").Select(un => un.Attribute("Surname")?.Value).ToList();
                var duplicates = list.GroupBy(x => x)
                    .Where(g => g.Count() > 1)
                    .Select(y => y.Key)
                    .ToList();
                if (duplicates.Count > 0)
                {
                    ModPlusAPI.Windows.MessageBox.Show("В списке содержатся одинаковые значения!", MessageBoxIcon.Alert);
                    e.Cancel = true;
                }
                else SaveToConfig();
            }
            catch (Exception exception)
            {
                ExceptionBox.Show(exception);
            }
        }
        // Выбор позиции в списке
        private void DgSurnames_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Активируем кнопку
            BtRemoveSurname.IsEnabled = true;
        }
    }
}