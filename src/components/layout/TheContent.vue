<template>
    <div class="content-wrap">
        <div class="content-area">
            <div class="content-title">
                <div class="title-row" v-if="mode == this.$resourceVn.MainScreen">
                    <div class="title">Chính sách lương</div>
                    <ButtonIcon :buttonName="this.$resourceVn.ButtonIconAddText" @click.native="clickAdd()"/>
                </div>
                <div class="title-row" v-if="mode == this.$resourceVn.AddScreen">
                    <div class="wrap-title">
                        <div class="icon-36 icon-wrap" @click="clickCancel()">
                            <div class="icon-20 icon-title"></div>
                        </div>
                        <div class="title">Thêm chính sách lương</div>
                    </div>
                    <div class="wrap-button-group">
                        <Button class="button-white" :buttonName="this.$resourceVn.ButtonCancelText" @click.native="clickCancel()"/>
                        <Button class="button-green" :buttonName="this.$resourceVn.ButtonSaveText"/>
                    </div>
                </div>
            </div>
            <div class="responsive-content-wrap">
                <div class="responsive-content-main">
                    <div class="content-body"> 
                    <div class="content-body-background">
                        <div class="content-body-tool" >
                            <div class="content-body-tool-left" v-if="mode == this.$resourceVn.MainScreen">
                                <InputSearch/>
                            </div>
                            <div class="content-body-tool-right" v-if="mode == this.$resourceVn.MainScreen">
                                <Dropdown width="230px" :listData="this.$resourceVn.Status" :typeDropdown="this.$resourceVn.DropdownStatus"/>
                                <DropdownSingle/>
                                <div class="filter" @click="showFilterPopup()">
                                    <div class="icon-20 icon-filter"></div>
                                </div>
                                <div v-click-outside="hidePopupEditColumn">
                                    <div class="setting" @click="showCustomeColumn()">
                                    <div class="icon-24 icon-setting"></div>
                                    </div>
                                    <CustomeColumn :showPopupEditColumn="showPopupEditColumn"/>
                                </div>
                            </div>
                            <div class="general-info-wrap" v-if="mode == this.$resourceVn.AddScreen">
                                <div class="general-info-title">
                                    {{ this.$resourceVn.GeneralInformation }}
                                </div>
                            </div>
                        </div>
                    </div>   
                    </div>
                    <div class="content-grid">
                        <DataGrid 
                        :listHeader="listHeader" 
                        :dataSource="dataSource"
                        v-if="mode == this.$resourceVn.MainScreen">
                            <template #Status="{ data }">
                                <div>{{ data.Status }}</div>
                            </template>
                        </DataGrid>
                        <div class="content-add-screen-wrap" v-if="mode == this.$resourceVn.AddScreen">
                            <div class="row-flex">
                                <div class="label-name">{{ this.$resourceVn.DepartmentApply }}<span class="red-span"> *</span></div>
                                <DropdownMultiple/>
                            </div>
                            <div class="row-flex">
                                <div class="label-name">{{ this.$resourceVn.PositionApply }}</div>
                                <Combobox :placeholder="this.$resourceVn.AllPositionApply"/>
                            </div>
                            <div class="row-flex">
                                <div class="label-name">{{ this.$resourceVn.EmployeeApply }}</div>
                                <Combobox :placeholder="this.$resourceVn.AllEmployeeApply"/>
                            </div>
                            <div class="row-flex">
                                <div class="label-name">{{ this.$resourceVn.PolicyName }}<span class="red-span"> *</span></div>
                                <Input/>
                            </div>
                            <div class="salary-component">
                                <div class="salary-component-text">{{ this.$resourceVn.SalaryComponent }}</div>
                            </div>
                            <div class="component-button-add">
                                <ButtonIcon class="button-icon-white" :buttonName="this.$resourceVn.ButtonIconAddComponentText" @click.native="clickAddComponent()"/>
                            </div>
                        </div>
                    </div>
                    <TheFooter v-if="mode == this.$resourceVn.MainScreen"/>
                </div>
                <div class="responsive-filter">
                    <FilterBox :filterShow="filterShow" @hideFilterPopup="hideFilterPopup"/>
                </div>
            </div>    
            <ModalBoxSalary :modalBoxShow="modalBoxShow" 
            @exitModalBox="exitModalBox"/>
        </div>
    </div>
</template>
<script>
import fakeData from '../../js/fake-data/data-grid-main'

// import { EventBus } from '../../main';
import ClickOutSide from "../../js/detectoutside"

import TheFooter from "../layout/TheFooter.vue"
import ButtonIcon from "../base/button-icon/BaseButtonIcon.vue"
import Button from "../../components/base/button/BaseButton.vue"
import DataGrid from "../base/data-grid/BaseDataGrid.vue"
import DropdownSingle from "../base/dropdown-single/BaseDropdownSingle.vue"
import Dropdown from '../base/dropdown/BaseDropdown.vue'
import DropdownMultiple from "../base/dropdown-multiple/BaseDropdownMultiple.vue"
import Combobox from '../../components/base/combobox/BaseCombobox.vue'
import Input from "../../components/base/input/BaseInput.vue"
import InputSearch from "../../components/base/input-search/BaseInputSearch.vue"
import ModalBoxSalary from "../../components/layout/TheModalBoxSalary.vue"
import CustomeColumn from "../../components/layout/CustomeColumn.vue"
import FilterBox from "../base/filter/Filter.vue"
export default {
    name: "TheContent",
    directives: {
        ClickOutSide
    },
    components: {
        TheFooter,
        ButtonIcon,
        Button,
        DataGrid,
        DropdownSingle,
        Dropdown,
        DropdownMultiple,
        Combobox,
        Input,
        InputSearch,
        ModalBoxSalary,
        CustomeColumn,
        FilterBox
    },
    data(){
        return {
            modalBoxShow: false,                //Trạng thái ẩn/hiện của modalbox, mặc định ẩn
            showPopupEditColumn: false,         //Trạng thái ẩn/hiện của popup setting "Tùy chỉnh cột"
            filterShow: false,                  //Trạng thái ẩn/hiện của popup "Bộ lọc"

            mode: this.$resourceVn.MainScreen, //Màn hình hiển thị (0: Màn chính, 1: Màn Thêm chính sách lương)

            listHeader: fakeData.listHeader,    //Dữ liệu các tiêu đề của grid
            dataSource: fakeData.dataSource,    //Dữ liệu của grid

        }
    },
    
    methods: {
        /**
         * @description Xử lý sự kiện click button "Thêm mới"
         * @date 17/9/2021
         * @createdBy LHTDung
         */
        clickAdd(){
            this.mode = this.$resourceVn.AddScreen;
        },

        /**
         * @description Xử lý sự kiện click button "Hủy bỏ" hoặc trở lại
         * @date 17/9/2021
         * @createdBy LHTDung
         */
        clickCancel(){
            this.mode = this.$resourceVn.MainScreen;
        },

        /**
         * @description Xử lý sự kiện click button "Thêm thành phần"
         * @date 18/9/2021
         * @createdBy LHTDung
         */
        clickAddComponent(){
            //Hiển thị modalbox 
            this.modalBoxShow = true;
        },

        /**
         * @description Ẩn modalbox
         * @date 18/9/2021
         * @createdBy LHTDung
         */
        exitModalBox(){
            this.modalBoxShow = false;
        },

        /**
         * @description Xử lý sự kiện click button Setting, ẩn/hiện popup "Tùy chỉnh cột"
         * @date 18/9/2021
         * @createdBy LHTDung
         */
        showCustomeColumn(){
            this.showPopupEditColumn = !this.showPopupEditColumn;
        },

        /**
         * @description Ẩn popup "Tùy chỉnh cột" khi bị click ngoài phần tử
         * @date 18/9/2021
         * @createdBy LHTDung
         */
        hidePopupEditColumn(){
            this.showPopupEditColumn = false;
        },

        /**
         * @description Hiện popup "Bộ lọc" khi click button filter
         * @date 19/9/2021
         * @createdBy LHTDung
         */
        showFilterPopup(){
            this.filterShow = !this.filterShow;
        },

        /**
         * @description Đóng "Bộ lọc" khi click button exit
         * @date 19/9/2021
         * @createdBy LHTDung
         */
        hideFilterPopup(){
            this.filterShow = false;
        }
    },
}
</script>
<style scoped>
    @import "../../css/layout/content.css";
</style>