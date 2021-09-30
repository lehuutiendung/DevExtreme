<template>
    <div class="content-area">
        <div class="content-title">
            <div class="title-row" v-if="mode == this.$resourceVn.MainScreen">
                <div class="title">Chính sách lương</div>
                <ButtonIcon :buttonName="this.$resourceVn.ButtonIconAddText" @click.native="clickAdd()"/>
            </div>
            <div class="title-row" v-if="mode == this.$resourceVn.AddScreen">
                <div class="wrap-title">
                    <div class="icon-36 icon-wrap back-add" 
                    @click="clickCancel()" id="back-add" 
                    @mouseenter="showTooltip($event)" @mouseleave="hideTooltip()">
                        <div class="icon-20 icon-title"></div>
                    </div>
                    <div class="title">Thêm chính sách lương</div>
                </div>
                <div class="wrap-button-group">
                    <Button class="button-white" :buttonName="this.$resourceVn.ButtonCancelText" @click.native="showPopup()"/>
                    <Button class="button-green save-tooltip" 
                    :buttonName="this.$resourceVn.ButtonSaveText" 
                    @click.native="clickSave()" @mouseenter.native="showTooltip($event)" @mouseleave.native="hideTooltip()"/>
                </div>
            </div>
            <!--------------------------- Xem chi tiết ---------------------->
            <div class="title-row" v-if="mode == this.$resourceVn.ViewScreen">
                <div class="wrap-title">
                    <div class="icon-36 icon-wrap back" 
                    @click="clickCancel()" id="back"
                    @mouseenter="showTooltip($event)" @mouseleave="hideTooltip()">
                        <div class="icon-20 icon-title"></div>
                    </div>
                    <div class="title">Xem {{ objectEdit.PolicyName }}</div>
                </div>
                <div class="wrap-button-group">
                    <Button class="button-green" 
                    :buttonName="this.$resourceVn.ButtonEditText" 
                    @click.native="clickButtonEdit()"/>
                    <div class="icon-36 button-3-dots" @click="clickThreeDots()" v-click-outside="hideGroupThreeDots">
                        <div class="icon-20 icon-button-3-dots"></div>
                    </div>
                    <div class="wrap-duplicate-delete-popup" :class="{'show-popup' : clickedThreeDots}">
                        <div class="item-popup-3-dots" @click="handleDuplicatePolicy()">
                            <div class="icon-20 icon-duplicate"></div>
                            <div>{{ this.$resourceVn.DuplicateText }}</div>
                        </div>
                        <div class="item-popup-3-dots" @click="handleDeleteInViewScreen()">
                            <div class="icon-20 icon-delete"></div>
                            <div>{{ this.$resourceVn.DeleteText }}</div>
                        </div>
                    </div>
                </div>
            </div>
            <!------------------------------ Sửa ---------------------------->
            <div class="title-row" v-if="mode == this.$resourceVn.EditScreen">
                <div class="wrap-title">
                    <div class="icon-36 icon-wrap" @click="clickCancel()">
                        <div class="icon-20 icon-title"></div>
                    </div>
                    <div class="title">Sửa {{ objectEdit.PolicyName }}</div>
                </div>
                <div class="wrap-button-group">
                    <Button class="button-white" :buttonName="this.$resourceVn.ButtonCancelText" @click.native="showPopup()"/>
                    <Button class="button-green save-tooltip" 
                    :buttonName="this.$resourceVn.ButtonSaveText" 
                    @click.native="saveEdit()" 
                    @mouseenter.native="showTooltip($event)" @mouseleave.native="hideTooltip()"/>
                </div>
            </div>
        </div>
        <div class="responsive-content-wrap">
            <div class="responsive-content-main" :class="{'fix-width-content-main' : filterShow}">
                <div class="content-body"> 
                <div class="content-body-background">
                    <div class="content-body-tool">
                        <div class="content-tool-without-check" v-if="totalChecked == 0 || mode == this.$resourceVn.AddScreen ">
                            <div class="content-body-tool-left" v-if="mode == this.$resourceVn.MainScreen">
                                <InputSearch @keyup.enter.native="handleInputSearch($event)"/>
                            </div>
                            <div class="content-body-tool-right" v-if="mode == this.$resourceVn.MainScreen">
                                <Dropdown width="230px" 
                                :listData="this.$resourceVn.Status" 
                                :typeDropdown="this.$resourceVn.DropdownStatus" 
                                @getValueStatusSearch="getValueStatusSearch($event)"/>
                                <DropdownSingle :treeDataSource="treeDataDepartment" 
                                @getValueDropdownSingle="getValueDropdownSingle($event)"/>
                                <div class="filter" @click="showFilterPopup()" id="filter" 
                                @mouseenter="showTooltip($event)" @mouseleave="hideTooltip()">
                                    <div class="icon-20 icon-filter"></div>
                                </div>
                                <div v-click-outside="hidePopupEditColumn">
                                    <div class="setting" @click="showCustomeColumn()" id="setting-column" 
                                    @mouseenter="showTooltip($event)" @mouseleave="hideTooltip()">
                                    <div class="icon-24 icon-setting"></div>
                                    </div>
                                    <CustomeColumn 
                                    :showPopupEditColumn="showPopupEditColumn" 
                                    :headers="headers"
                                    @updateListHeaderGrid="updateListHeaderGrid"
                                    @updateListHeaderDefault="updateListHeaderDefault"/>
                                </div>
                            </div>
                            <!-- THÔNG TIN CHUNG -->
                            <div class="general-info-wrap" v-if="mode == this.$resourceVn.AddScreen 
                            || mode == this.$resourceVn.ViewScreen 
                            || mode == this.$resourceVn.EditScreen">
                                <div class="general-info-title">
                                    {{ this.$resourceVn.GeneralInformation }}
                                </div>
                            </div>
                        </div>
                        <div class="content-tool-has-check" v-if="mode == this.$resourceVn.MainScreen && totalChecked > 0">
                            <div class="number-choosed">
                                <div>{{ this.$resourceVn.Choosed}} <span>{{totalChecked}}</span></div>
                            </div>
                            <div class="stop-apply" @click="clearSelectionDataGrid()">{{ this.$resourceVn.UnChecked }}</div>
                            <ButtonIcon class="button-icon-orange" 
                                v-if="showButtonStopApply"
                                :typeButton="this.$resourceVn.ButtonIconApply" 
                                :buttonName="this.$resourceVn.ButtonIconApplyText" 
                                @click.native="clickStopApplyMultiple()"
                            />
                            <div class="space" v-if="showButtonStopApply"></div>
                            <ButtonIcon class="button-icon-green" 
                                v-if="showButtonApply"
                                :typeButton="this.$resourceVn.ButtonIconActiveApply" 
                                :buttonName="this.$resourceVn.ButtonIconActiveApplyText" 
                                @click.native="clickApplyMultiple()"
                            />
                            <div class="space" v-if="showButtonApply"></div>
                            <ButtonIcon class="button-icon-red" 
                                :typeButton="this.$resourceVn.ButtonIconDelete" 
                                :buttonName="this.$resourceVn.ButtonIconDeleteText" 
                                @click.native="clickDeleteMultiple()"
                            />
                        </div>
                    </div>
                </div>   
                </div>
                <div class="content-grid">
                    <DataGrid 
                    :listHeader="listHeader" 
                    :dataSource="dataSource"
                    @moveToViewScreen="moveToViewScreen($event)"
                    @numberOfCheckedGrid="numberOfCheckedGrid($event)"
                    @deleteSingleRow="deleteSingleRow($event)"
                    v-if="mode == this.$resourceVn.MainScreen">
                        <template #Status="{ data }">
                            <div class="wrap-template-status">
                                <div class="dot-status" :class="{'dot-stop-apply' : data.StatusName == 'Ngừng áp dụng' ? true : false}"></div>
                                <div class="status-text" :class="{'stop-apply-text' : data.StatusName == 'Ngừng áp dụng' ? true : false}">{{ data.StatusName }}</div>
                            </div>
                        </template>
                    </DataGrid>
                    <div class="content-add-screen-wrap" v-if="mode == this.$resourceVn.AddScreen 
                    || mode == this.$resourceVn.ViewScreen 
                    || mode == this.$resourceVn.EditScreen">
                        <div class="row-flex">
                            <div class="label-name">{{ this.$resourceVn.DepartmentApply }}<span class="red-span"> *</span></div>
                            <!-- Hiển thị dropdown với màn thêm mới-->
                            <DropdownMultiple 
                            ref="dropdownMultipleRequired" 
                            v-model="objectEdit.DepartmentName"
                            @updateValueDropdownMulti="updateValueDropdownMulti($event)"
                            :treeDataSource="treeDataDepartment"
                            v-if="mode == this.$resourceVn.AddScreen || mode == this.$resourceVn.EditScreen"
                            :tabIndex="1"
                            />
                            <!-- Hiển thị với màn xem chi tiết -->
                            <div class="view-object" v-if="mode == this.$resourceVn.ViewScreen">
                                {{ objectEdit.DepartmentName }}
                            </div>
                        </div>
                        <div class="validate-text" v-if="errorValidateDropdown">{{ this.$resourceVn.ValidateEmpty }}</div>
                        <div class="row-flex">
                            <div class="label-name">{{ this.$resourceVn.PositionApply }}</div>
                            <!-- Hiển thị combobox với màn thêm mới-->
                            <Combobox 
                            :placeholder="this.$resourceVn.AllPositionApply" 
                            :listObjectData="dataPosition"
                            :nameCombobox="this.$resourceVn.POSITION"
                            v-model="objectEdit.PositionName"
                            v-if="mode == this.$resourceVn.AddScreen || mode == this.$resourceVn.EditScreen"
                            :tabIndex="2"
                            />
                            <!-- Hiển thị với màn xem chi tiết -->
                            <div class="view-object" v-if="mode == this.$resourceVn.ViewScreen">
                                {{ objectEdit.PositionName }}
                            </div>
                        </div>
                        <div class="row-flex">
                            <div class="label-name">{{ this.$resourceVn.EmployeeApply }}</div>
                            <!-- Hiển thị combobox với màn thêm mới-->
                            <Combobox 
                            :placeholder="this.$resourceVn.AllEmployeeApply"
                            :listObjectData="dataEmployee" 
                            :nameCombobox="this.$resourceVn.EMPLOYEE"
                            v-model="objectEdit.FullName"
                            v-if="mode == this.$resourceVn.AddScreen || mode == this.$resourceVn.EditScreen"
                            :tabIndex="3"
                            />
                            <!-- Hiển thị với màn xem chi tiết -->
                            <div class="view-object" v-if="mode == this.$resourceVn.ViewScreen">
                                {{ objectEdit.FullName }}
                            </div>
                        </div>
                        <div class="row-flex">
                            <div class="label-name">{{ this.$resourceVn.PolicyName }}<span class="red-span"> *</span></div>
                            <!-- Hiển thị input với màn thêm mới-->
                            <Input ref="inputRequired" 
                            width="748px!important"
                            @blur.native="checkValidate($event)" 
                            @focus.native="inputValidate($event)"
                            v-model="objectEdit.PolicyName"
                            v-if="mode == this.$resourceVn.AddScreen || mode == this.$resourceVn.EditScreen"
                            :tabIndex="4"
                            />
                            <!-- Hiển thị với màn xem chi tiết -->
                            <div class="view-object" v-if="mode == this.$resourceVn.ViewScreen">
                                {{ objectEdit.PolicyName }}
                            </div>
                        </div>
                        <div class="row-flex" v-if="mode == this.$resourceVn.ViewScreen || mode == this.$resourceVn.EditScreen">
                            <div class="label-name">{{ this.$resourceVn.StatusTextForm }}</div>
                            <div class="wrap-status">
                                <div class="wrap-radio-text">
                                    <div class="icon-32 radio-button" @click="checkRadioAppling()">
                                        <div class="icon-20 radio-button-uncheck" 
                                        :class="{'radio-button-check' : objectEdit.Status == this.$resourceVn.Status[1].Value ? true: false}"></div>
                                    </div>
                                    <div>{{ this.$resourceVn.Status[1].Text }}</div>
                                </div>
                                <div class="wrap-radio-text">
                                    <div class="icon-32 radio-button" @click="checkRadioStopApply()">
                                        <div class="icon-20 radio-button-uncheck"
                                        :class="{'radio-button-check' : objectEdit.Status == this.$resourceVn.Status[2].Value ? true: false}"></div>
                                    </div>
                                    <div>{{ this.$resourceVn.Status[2].Text }}</div>
                                </div>
                            </div>
                        </div>

                        <div class="validate-text" v-if="errorValidateInput">{{ this.$resourceVn.ValidateEmpty }}</div>
                        <!----------------------------- THÀNH PHẦN LƯƠNG --------------------------------->
                        <div class="salary-component">
                            <div class="salary-component-text">{{ this.$resourceVn.SalaryComponent }}</div>
                        </div>
                        <div class="group-grid-salary-component" v-if="this.listChoosedComponent.length > 0">
                            <div class="grid-salary-component-title">
                                <div class="col-1">{{ this.$resourceVn.ComponentNameTitle }}</div>
                                <div class="col-2">{{ this.$resourceVn.ComponentCodeTitle }}</div>
                                <div class="col-3">{{ this.$resourceVn.FormulaTitle }}</div>
                            </div>
                            <draggable class="draggable" v-model="listChoosedComponent">
                                <transition-group>
                                    <div class="group-input-component" v-for="(item, index) in listChoosedComponent" :key="index">
                                        <div class="icon-20 icon-move"></div>
                                        <Input width="30%!important" class="noHover" 
                                        :value="item.ComponentName" 
                                        disabled :style="{background: '#E8EAEF'}"/>
                                        <div class="space"></div>
                                        <Input width="20%!important" class="noHover" 
                                        :value="item.ComponentCode" 
                                        disabled :style="{background: '#E8EAEF'}"/>
                                        <div class="space"></div>
                                        <Input width="50%!important" v-model="item.ComponentValue"/>
                                        <div class="icon-20 create-group"></div>
                                        <div class="icon-20 icon-delete-component" @click="deleteComponent(item)"></div>
                                    </div>
                                </transition-group>
                            </draggable>
                        </div>
                        <div class="component-button-add" v-if="mode == this.$resourceVn.AddScreen || mode == this.$resourceVn.EditScreen">
                            <ButtonIcon class="button-icon-white" 
                            :buttonName="this.$resourceVn.ButtonIconAddComponentText" 
                            @click.native="clickAddComponent()"/>
                        </div>
                    </div>
                </div>
                <TheFooter v-if="mode == this.$resourceVn.MainScreen" 
                :totalRecord="totalRecord" 
                :startIndex="startIndex" 
                :endIndex="endIndex" 
                @updatePrevPageNumber="updatePrevPageNumber" 
                @updateNextPageNumber="updateNextPageNumber"/>
            </div>
            <div class="responsive-filter">
                <FilterBox :filterShow="filterShow" 
                :dataPosition="dataPosition" 
                :dataEmployee="dataEmployee" 
                @hideFilterPopup="hideFilterPopup"/>
            </div>
        </div>    
        <SalaryPolicyDetail 
        v-if="modalBoxShow"
        :modalBoxShow="modalBoxShow" 
        :updateItemSource="updateItemSource"
        :listChoosedComponent="listChoosedComponent"
        :mode="mode"
        @getListComponent="getListComponent($event)"
        @exitModalBox="exitModalBox"/>
        <Popup 
        :mode="mode"
        @clickSaveEditPopup="clickSaveEditPopup"
        @clickSaveAddPopup="clickSaveAddPopup"
        @moveToMainScreen="moveToMainScreen" 
        @acceptDelete="acceptDelete" 
        @changeStatusSingle="changeStatusSingle($event)" 
        @changeStatusMulti="changeStatusMulti"
        @activeStatusMulti="activeStatusMulti"/>
        <ToastMessenger/>
        <Tooltip/>
        <Loading :showLoading="showLoading"/>
    </div>
</template>
<script>
import fakeData from '../../js/fake-data/data-grid-main'
import axios from "axios";
import ClickOutSide from "../../js/detectoutside"
import { EventBus } from "../../main"
import draggable from 'vuedraggable'
import TheFooter from "../../components/layout/TheFooter.vue"
import ButtonIcon from "../../components/base/button-icon/BaseButtonIcon.vue"
import Button from "../../components/base/button/BaseButton.vue"
import DataGrid from "../../components/base/data-grid/BaseDataGrid.vue"
import DropdownSingle from "../../components/base/dropdown-single/BaseDropdownSingle.vue"
import Dropdown from '../../components/base/dropdown/BaseDropdown.vue'
import DropdownMultiple from "../../components/base/dropdown-multiple/BaseDropdownMultiple.vue"
import Combobox from '../../components/base/combobox/BaseCombobox.vue'
import Input from "../../components/base/input/BaseInput.vue"
import InputSearch from "../../components/base/input-search/BaseInputSearch.vue"
import SalaryPolicyDetail from "../salary/SalaryPolicyDetail.vue"
import CustomeColumn from "../../components/layout/CustomeColumn.vue"
import FilterBox from "../../components/base/filter/Filter.vue"
import Popup from "../../components/base/popup/BasePopup.vue"
import ToastMessenger from "../../components/base/toast-message/BaseToastMessenger.vue"
import Tooltip from "../../components/base/tooltip/BaseTooltip.vue"
import Loading from "../../components/base/loading/BaseLoading.vue"
export default {
    name: "SalaryPolicyList",
    directives: {
        ClickOutSide
    },
    components:{
        draggable,
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
        SalaryPolicyDetail,
        CustomeColumn,
        FilterBox,
        Popup,
        ToastMessenger,
        Tooltip,
        Loading
    },
    data(){
        return {
            modalBoxShow: false,                //Trạng thái ẩn/hiện của modalbox, mặc định ẩn
            showPopupEditColumn: false,         //Trạng thái ẩn/hiện của popup setting "Tùy chỉnh cột"
            filterShow: false,                  //Trạng thái ẩn/hiện của popup "Bộ lọc"
            errorValidateInput: false,          //Validate input
            errorValidateDropdown: false,       //Validate dropdown
            clickedThreeDots: false,            //Click button 3 dots trong màn xem chi tiết để thực hiện "Nhân bản, Xóa"
            mode: this.$resourceVn.MainScreen,  //Màn hình hiển thị (0: Màn chính, 1: Màn Thêm chính sách lương)
            totalChecked: 0,                    //Tổng số dòng được checked trong Data Grid
            objectEdit: {},                     //Object chứa dữ liệu được click xem chi tiết 
            listHeader: fakeData.listHeader,    //Dữ liệu các tiêu đề của grid
            headers: fakeData.listHeader,
            headersDefault: fakeData.headerDefault,
            dataSource: [],                     //Dữ liệu của grid
            treeDataDepartment: [],             //Data cho Dropdown (Đơn vị)
            dataPosition: [],                   //Data cho Combobox Tag (Vị trí)
            dataEmployee: [],                   //Data cho Combobox Tag (Nhân viên)
            updateItemSource: {},               //Object chứa item "Thành phần lương" sau khi xóa (Cập nhật vào Data Grid vào bảng Thành phần)
            listChoosedComponent: [],           //Danh sách chứa các item "Thành phần Lương" sẵn sàng cho thêm mới
            queueDelete: [],                    //Danh sách chứa các Id chính sách cần xóa
            queueChecked: [],                   //Danh sách chứa các item được check trong datagrid chính sách
            totalPage: 0,                       //Tổng số trang
            totalRecord: 0,                     //Tổng số bản ghi
            pageSize: 15,                       //Kích thước trang
            pageNumber: 1,                      //Trang lấy dữ liệu
            maxPages: 5,                        //Số trang hiển thị
            startIndex: 0,                      //Bản ghi bắt đầu trong trang
            endIndex: 0,                        //Bản ghi kết thúc trong trang
            inputSearch: "",                    //Filter theo tên chính sách
            statusSearch: "",                   //Filter theo trạng thái
            departmentSearch: "",               //Filter theo cơ cấu tổ chức (đơn vị)
            checkForm: false,                   //Check trước khi thêm mới, nếu checkForm == true -> Thỏa mãn các điều kiện có thể gọi API
            showLoading: false,                 //Ẩn loading
            showButtonStopApply: false,         //Ẩn - hiện button Ngừng áp dụng
            showButtonApply: false,             //Ẩn - hiện button Đang áp dụng
        }
    },
    mounted() {
        /**
         * @description Nhận emit từ Combobox Tag, thay đổi giá trị của object chỉnh sửa
         * @created LHTDung
         * @date 22/09/2021
         */
        EventBus.$on('updateValueCombobox', (nameCombobox, value) => {
            if(nameCombobox == this.$resourceVn.POSITION){
                let stringPosition = '';
                value.forEach(item => {
                    stringPosition += item;
                    stringPosition += "; ";
                });
                // Xóa cặp kí tự "; " cuối cùng của chuỗi
                this.objectEdit.PositionName = stringPosition.slice(0, -2);
            }
            if(nameCombobox == this.$resourceVn.EMPLOYEE){
                let stringEmployee = '';
                value.forEach(item => {
                    stringEmployee += item;
                    stringEmployee += "; ";
                });
                // Xóa cặp kí tự "; " cuối cùng của chuỗi
                this.objectEdit.FullName = stringEmployee.slice(0, -2);
            }
        })

        /**
         * @description Cập nhật lại số bản ghi/trang (Nhận $emit từ Dropdown)
         * @created LHTDung
         * @date 23/09/2021
         */
        EventBus.$on('getValueFromDropdownCustomize', (value) => {
            //Nếu nhận emit được là số thì gán giá trị cho pageSize
            if(isNaN(parseInt(value,10)) == false){
                this.pageSize = parseInt(value, 10);
            }
        })

        /************************************************ Gọi API ************************************************/
        /**
         * @description Gọi API lấy tất cả dữ liệu của Đơn vị
         * @created LHTDung
         * @date 22/09/2021
         */
        axios.get(this.$resourceVn.URL_DEPARTMENT_GETALL)
        .then(res => {
          this.treeDataDepartment = res.data;
        })
        .catch(err => {
          console.error(err); 
        })

        /**
         * @description Gọi API lấy tất cả dữ liệu của Vị trí
         * @created LHTDung
         * @date 22/09/2021
         */
        axios.get(this.$resourceVn.URL_POSITION_GETALL)
        .then(res => {
          this.dataPosition = res.data;
        })
        .catch(err => {
          console.error(err); 
        })

        /**
         * @description Gọi API lấy tất cả dữ liệu của Nhân viên
         * @created LHTDung
         * @date 22/09/2021
         */
        axios.get(this.$resourceVn.URL_EMPLOYEE_GETALL)
        .then(res => {
          this.dataEmployee = res.data;
        })
        .catch(err => {
          console.error(err); 
        })
        
        /**
         * @description Gọi API lấy dữ liệu trang đầu tiên của Chính sách
         * @created LHTDung
         * @date 23/09/2021
         */
        this.callAPIFilterPolicy(this.pageSize, this.pageNumber, this.inputSearch, this.statusSearch, this.departmentSearch);
        
        /**
         * @description Thêm sự kiện phím tắt
         */
        document.addEventListener("keydown", this.keyDoSave);

    },
    
    beforeDestroy() {
        document.removeEventListener("keydown", this.keyDoSave);
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
            EventBus.$emit('hideTooltip');
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
        },
        
        /**
         * @description Nhận $emit danh sách được check từ Data Grid -> Lấy tổng số dòng được check
         * @date 20/9/2021
         * @createdBy LHTDung
         */
        numberOfCheckedGrid(listChecked){
            this.totalChecked = listChecked.length;
            this.queueChecked = listChecked;
        },

        /**
         * @description Validate Input trong form thêm mới
         * @date 20/9/2021
         * @createdBy LHTDung
         */
        checkValidate(e){
            if(e.target.value == ""){
                this.errorValidateInput = true;
                this.$refs.inputRequired.$el.style.border = this.$resourceVn.BorderValidate;
            }else{
                this.errorValidateInput = false;
                this.$refs.inputRequired.$el.style.border = this.$resourceVn.BorderNormal;
            }
        },
        
        /**
         * @description Focus Input thay đổi border và ẩn validate
         * @date 20/9/2021
         * @createdBy LHTDung
         */
        inputValidate(e){
            console.log(e);
            this.errorValidateInput = false;
            this.$refs.inputRequired.$el.style.border = this.$resourceVn.BorderActive;
        },

        /**
         * @description Focus vào trường đầu tiên trong form
         * @date 20/9/2021
         * @createdBy LHTDung
         */
        focusFirstField(){
            this.$refs.dropdownMultipleRequired.$el.children[0].classList.add(this.$resourceVn.ClassFocus);
        },

        /**
         * @description Validate trước khi gọi api thêm mới
         * @created LHTDung
         * @date 23/09/2021
         */
        validateForm(){
            //Hiển thị validate và border cho trường dropdown multiple "Đơn vị áp dụng"
            if(this.$refs.dropdownMultipleRequired.$el.children[0].classList.contains(this.$resourceVn.ClassEmpty)){
                this.errorValidateDropdown = true;
                this.$refs.dropdownMultipleRequired.$el.children[0].classList.add(this.$resourceVn.BorderErrorValidate);
            }else{
                this.errorValidateDropdown = false;
                this.$refs.dropdownMultipleRequired.$el.children[0].classList.remove(this.$resourceVn.BorderErrorValidate);
            }

            //Hiển thị validate và border cho trường input "Tên chính sách"
            if(this.$refs.inputRequired.$el.value == ""){
                this.errorValidateInput = true;
                this.$refs.inputRequired.$el.style.border = this.$resourceVn.BorderValidate;
            }

            //Validate bắt buộc phải có ít nhất 1 thành phần lương trong danh sách khi thêm mới
            if(this.errorValidateDropdown == false && this.errorValidateInput == false){
                if(this.listChoosedComponent.length == 0){
                    EventBus.$emit('showPopupAddForm', this.$resourceVn.POPUP_ADD_TYPE);
                }else{
                    this.checkForm = true;
                    // Chuyển Array component thành JSON (Lưu ở database)
                    this.objectEdit.ComponentName = JSON.stringify(this.listChoosedComponent);
                    // Bỏ trống trường vị trí áp dụng -> Vị trí áp dụng chính sách: Tất cả 
                    if(this.objectEdit.PositionName == null || this.objectEdit.PositionName == ""){
                        this.objectEdit.PositionName = this.$resourceVn.AllPositionApply;
                    }
                    // Bỏ trống trường nhân viên áp dụng -> Nhân viên áp dụng chính sách: Tất cả 
                    if(this.objectEdit.FullName == null || this.objectEdit.FullName == ""){
                        this.objectEdit.FullName = this.$resourceVn.AllEmployeeApply;
                    }
                }
            }
        },

        /*********************************** GỌI API THÊM MỚI *********************************/
        /**
         * @description Xử lý sự kiện click button "Lưu"
         * @date 20/9/2021
         * @createdBy LHTDung
         */
        clickSave(){
            this.validateForm();
            if(this.checkForm == true){
                // Đặt trạng thái cho chính sách "Đang hoạt động"
                this.objectEdit.Status = 1;
                
                // Gọi API thực hiện thêm mới
                axios.post(this.$resourceVn.URL_POLICY_COMMON, this.objectEdit)
                .then(res => {
                    console.log(res);
                    this.objectEdit = {};
                })
                .then(() => {
                    this.mode = this.$resourceVn.MainScreen;
                    this.callAPIFilterPolicy(this.pageSize, this.pageNumber, this.inputSearch, this.statusSearch, this.departmentSearch);
                    this.checkForm = false;
                    EventBus.$emit('hidePopup');
                    EventBus.$emit('showToast', this.$resourceVn.ButtonIconAddText, this.$resourceVn.TOAST_SUCCESS);
                })
                .catch(err => {
                    console.error(err); 
                })
            }
        },

        /**
         * @description Xử lý sự kiện click vào từng dòng của data grid -> Xem chi tiết
         * @date 20/9/2021
         * @createdBy LHTDung
         */
        moveToViewScreen(e){
            this.mode = this.$resourceVn.ViewScreen;

            //Gọi API lấy chi tiết bản ghi chính sách
            axios.get(`https://localhost:44330/api/Policies/${e.PolicyId}`)
            .then(res => {
                this.objectEdit = res.data;
            })
            .then(() => {
                this.listChoosedComponent = JSON.parse(this.objectEdit.ComponentName);
            })
            .catch(err => {
                console.error(err); 
            })
        },

        /*********************************** GỌI API SỬA/CẬP NHẬT *********************************/
        /**
         * @description API cập nhật chính sách
         * @created LHTDung
         * @date 24/09/2021
         */
        callAPIPutPolicy(object){
            axios.put(`https://localhost:44330/api/Policies/${object.PolicyId}`, object)
            .then(res => {
                console.log(res);
                this.mode = this.$resourceVn.MainScreen;
                this.callAPIFilterPolicy(this.pageSize, this.pageNumber, this.inputSearch, this.statusSearch, this.departmentSearch);
                this.checkForm = false;
            }).then( () => {
                EventBus.$emit('showToast', this.$resourceVn.UPDATE_TEXT, this.$resourceVn.TOAST_SUCCESS);
            })
            .catch(err => {
                console.error(err); 
            })
        },

        /**
         * @description Xử lý sự kiện Click button Lưu (Sửa form)
         * @created LHTDung
         * @date 24/09/2021
         */
        saveEdit(){
            this.validateForm();
            if(this.checkForm == true){
                let arrayListDepartment = this.objectEdit.DepartmentName;
                if(Array.isArray(arrayListDepartment)){
                    let stringListDepartment = "";
                    arrayListDepartment.forEach(item => {
                    stringListDepartment += item;
                    stringListDepartment += '; ' ;
                    });
                    stringListDepartment = stringListDepartment.slice(0, -2);
                    this.objectEdit.DepartmentName = stringListDepartment;
                }
                this.callAPIPutPolicy(this.objectEdit);
            }
        },
        
        /**
         * @description Xử lý sự kiện click vào button 3 chấm -> Hiển thị Nhân bản/Xóa
         * @date 20/9/2021
         * @createdBy LHTDung
         */
        clickThreeDots(){
            this.clickedThreeDots = !this.clickedThreeDots;
        },

        /**
         * @description Xử lý sự kiện click vào button "Sửa" -> Bind dữ liệu vào form
         * @date 20/9/2021
         * @createdBy LHTDung
         */
        clickButtonEdit(){
            this.mode = this.$resourceVn.EditScreen;
            //Vị trí áp dụng của chính sách là tất cả -> Gán bằng rỗng để không hiện TagBox trong Combobox
            if(this.objectEdit.PositionName == this.$resourceVn.AllPositionApply){
                this.objectEdit.PositionName = "";
            }
            //Nhân viên áp dụng của chính sách là tất cả -> Gán bằng rỗng để không hiện TagBox trong Combobox
            if(this.objectEdit.FullName == this.$resourceVn.AllEmployeeApply){
                this.objectEdit.FullName = "";
            }
            // Tách chuỗi DepartmentName thành mảng các Đơn vị để có thể bind dữ liệu với DxDropdownMultiple
            this.objectEdit.DepartmentName = this.objectEdit.DepartmentName.split("; ");
            // this.objectEdit.DepartmentName.pop();
        },

        /**
         * @description Nhận $emit thay đổi trạng thái (thay đổi đơn ) của chính sách từ Popup
         * @created LHTDung
         * @date 24/09/2021
         */
        changeStatusSingle(data){
            let policyUpdate = data;
            // Nếu trạng thái là "Đang áp dụng" -> Chuyển thành "Ngừng áp dụng"
            if(data.Status == this.$resourceVn.Status[1].Value){
                policyUpdate.Status = this.$resourceVn.Status[2].Value;
            }else{
                // Nếu trạng thái là "Ngừng áp dụng" -> Chuyển thành "Đang áp dụng"
                policyUpdate.Status = this.$resourceVn.Status[1].Value;
            }
            // Gọi API cập nhật lại dữ liệu
            this.callAPIPutPolicy(policyUpdate);
        },

        /**
         * @description Xử lý sự kiện click button "Ngưng áp dụng" nhiều chính sách
         * @created LHTDung
         * @date 24/09/2021
         */
        clickStopApplyMultiple(){
            EventBus.$emit('showPopupStopApplyMultiple', this.$resourceVn.POPUP_STOPAPPLY_MULTI_TYPE);
        },

        /**
         * @description Xử lý sự kiện click button "Đang áp dụng" nhiều chính sách
         * @created LHTDung
         * @date 29/09/2021
         */
        clickApplyMultiple(){
            EventBus.$emit('showPopupApplyMultiple', this.$resourceVn.POPUP_APPLY_MULTI_TYPE);
        },

        /**
         * @description Nhận $emit "Đồng ý" ngừng áp dụng nhiều chính sách từ Popup
         * @created LHTDung
         * @date 24/09/2021
         */
        changeStatusMulti(){
            // Biến đếm kiểm tra cập nhật xong tất cả các đối tượng
            let countItemChanged = 0;
            for(let i = 0 ; i < this.queueChecked.length; i++){
                let item = this.queueChecked[i];
                item.Status = this.$resourceVn.Status[2].Value;
                axios.put(`https://localhost:44330/api/Policies/${item.PolicyId}`, item)
                .then(res => {
                    console.log(res);
                    countItemChanged++;
                }).then( () => {
                    // Khi cập nhật xong các đối tượng => Gọi lại API phân trang làm mới dữ liệu
                    if(countItemChanged == this.queueChecked.length){
                        this.callAPIFilterPolicy(this.pageSize, this.pageNumber, this.inputSearch, this.statusSearch, this.departmentSearch);
                    }
                    EventBus.$emit('showToast', this.$resourceVn.UPDATE_TEXT, this.$resourceVn.TOAST_SUCCESS);
                })
                .catch(err => {
                    console.error(err); 
                })
            }
        },

        /**
         * @description Nhận $emit "Đồng ý" đang áp dụng nhiều chính sách từ Popup
         * @created LHTDung
         * @date 29/09/2021
         */
        activeStatusMulti(){
            // Biến đếm kiểm tra cập nhật xong tất cả các đối tượng
            let countItemChanged = 0;
            for(let i = 0 ; i < this.queueChecked.length; i++){
                let item = this.queueChecked[i];
                item.Status = this.$resourceVn.Status[1].Value;
                axios.put(`https://localhost:44330/api/Policies/${item.PolicyId}`, item)
                .then(res => {
                    console.log(res);
                    countItemChanged++;
                }).then( () => {
                    // Khi cập nhật xong các đối tượng => Gọi lại API phân trang làm mới dữ liệu
                    if(countItemChanged == this.queueChecked.length){
                        this.callAPIFilterPolicy(this.pageSize, this.pageNumber, this.inputSearch, this.statusSearch, this.departmentSearch);
                    }
                    EventBus.$emit('showToast', this.$resourceVn.UPDATE_TEXT, this.$resourceVn.TOAST_SUCCESS);
                })
                .catch(err => {
                    console.error(err); 
                })
            }
        },

        /**
         * @description Hiển thị popup cảnh báo khi muốn hủy form đang chỉnh chỉnh sửa
         * @date 21/9/2021
         * @createdBy LHTDung
         */
        showPopup(){
            if( (this.mode == this.$resourceVn.AddScreen || this.mode == this.$resourceVn.EditScreen ) && Object.keys(this.objectEdit).length > 0 ){
                EventBus.$emit('showPopupCancelEditForm', this.$resourceVn.POPUP_EXIT_EDIT_TYPE);
            }else{
                this.clickCancel();
            }
        },

        /**
         * @description Nhận emit từ popup và chuyển về màn chính, (main screen)
         * @date 21/9/2021
         * @createdBy LHTDung
         */
        moveToMainScreen(){
            this.mode = this.$resourceVn.MainScreen;
        },

        /*************************************** THÀNH PHẦN LƯƠNG **************************************/
        /**
         * @description Nhận danh sách "Thành phần lương" được chọn từ Datagrid
         * @date 21/9/2021
         * @createdBy LHTDung
         */
        getListComponent(e){
            // this.listChoosedComponent = e;
            e.forEach(item => {
                this.listChoosedComponent.push(item);
            });
        },

        /**
         * @description Click button "Xóa" thành phần lương tại Thêm chính sách
         * @date 21/9/2021
         * @update 22/9/2021
         * @createdBy LHTDung
         */
        deleteComponent(value){
            this.updateItemSource = value;
            let index = this.listChoosedComponent.indexOf(value);
            if(index > - 1){
                this.listChoosedComponent.splice(index, 1);
            }
        },

        /**
         * @description Nhận emit thay đổi value từ Dropdown Multiple (v-model Dropdown Multiple)
         * @date 22/9/2021
         * @createdBy LHTDung
         */
        updateValueDropdownMulti(e){
            let stringListDepartment = "";
            e.forEach(nodeDropdown => {
                stringListDepartment += nodeDropdown.text;
                stringListDepartment += '; ' ;
            });
            // Xóa cặp kí tự "; " cuối cùng của chuỗi
            this.objectEdit.DepartmentName = stringListDepartment.slice(0, -2);
        },

        /**
         * @description Xử lý phân trang
         * @created LHTDung
         * @date 23/9/2021
         */
        paginate(totalRecord, pageNumber, pageSize, maxPages, totalPage){
            // đảm bảo trang hiện tại không nằm ngoài phạm vi
            if (pageNumber < 1) {
                pageNumber = 1;
            } else if (pageNumber > totalPage) {
                pageNumber = totalPage;
            }

            let startPage, endPage;
            // Trường hợp tổng số trang bé hơn số trang hiển thị tối đa
            if (totalPage <= maxPages) {
                startPage = 1;
                endPage = totalPage;
            } else {
                // tổng số trang nhiều hơn tối đa => tính trang bắt đầu và trang kết thúc
                let maxPagesBeforeCurrentPage = Math.floor(maxPages / 2);
                let maxPagesAfterCurrentPage = Math.ceil(maxPages / 2) - 1;
                if (pageNumber <= maxPagesBeforeCurrentPage) {
                    // trang hiện tại gần đầu
                    startPage = 1;
                    endPage = maxPages;
                } else if (pageNumber + maxPagesAfterCurrentPage >= totalPage) {
                    // trang hiện tại gần cuối
                    startPage = totalPage - maxPages + 1;
                    endPage = totalPage;
                } else {
                    // trang hiện tại nằm ở vùng giữa
                    startPage = pageNumber - maxPagesBeforeCurrentPage;
                    endPage = pageNumber + maxPagesAfterCurrentPage;
                }
            }

            // tính chỉ số của bản ghi bắt đầu và kết thúc
            let startIndex = (pageNumber - 1) * pageSize;
            let endIndex = Math.min(startIndex + pageSize - 1, totalRecord - 1);

            // tạo một mảng các trang
            let pages = Array.from(Array((endPage + 1) - startPage).keys()).map(i => startPage + i);
            var obj = {
                totalRecord: totalRecord,
                pageNumber: pageNumber,
                pageSize: pageSize,
                totalPage: totalPage,
                startPage: startPage,
                endPage: endPage,
                startIndex: startIndex,
                endIndex: endIndex,
                pages: pages
            }
            return obj;
        },

        /**
         * @description Gọi API thực hiện phân trang, tìm kiếm (Chính sách)
         */
        callAPIFilterPolicy(pageSize, pageNumber, filter, status, department){
            this.showLoading = true;
            axios.get(`https://localhost:44330/api/Policies/Filter?pageSize=${pageSize}&pageNumber=${pageNumber}&filter=${filter}&status=${status}&departmentName=${department}`)
            .then(res => {
                this.totalPage = res.data.TotalPage;
                this.totalRecord = res.data.TotalRecord;
                this.dataSource = res.data.Data;
                let objPage = this.paginate(this.totalRecord, this.pageNumber, this.pageSize, this.maxPages, this.totalPage);
                this.startIndex = objPage.startIndex;
                this.endIndex = objPage.endIndex;
                this.showLoading = false;
            })
            .catch(err => {
                console.error(err); 
            })
        },
        
        /**
         * @description Cập nhật trang khi click trở về trước 1 trang
         * @created LHTDung
         * @date 23/09/2021
         */
        updatePrevPageNumber(){
            if(this.pageNumber > 1){
                this.pageNumber--;
            }else{
                this.pageNumber = 1;
            }
        },

        /**
         * @description Cập nhật trang khi click next 1 trang
         * @created LHTDung
         * @date 23/09/2021
         */
        updateNextPageNumber(){
            if(this.pageNumber < this.totalPage){
                this.pageNumber++;
            }else{
                this.pageNumber = this.totalPage;
            }
        },
        
        /**
         * @description Xử lý sự kiện tìm kiếm Chính sách (Input)
         * @created LHTDung
         * @date 23/09/2021
         */
        handleInputSearch(e){
            this.inputSearch = e.target.value;
            this.pageNumber = 1;
            this.callAPIFilterPolicy(this.pageSize, this.pageNumber, this.inputSearch, this.statusSearch, this.departmentSearch);
        },

        /**
         * @description Nhận giá trị từ dropdown Trạng thái -> Truyền giá trị vào statusSearch
         * @created LHTDung
         * @date 23/09/2021
         */
        getValueStatusSearch(e){
            if(e == 0){
                this.statusSearch = "";
            }else{
                this.statusSearch = e;
            }
        },

        /**
         * @description Nhận giá trị từ dropdown đơn vị -> Truyền giá trị vào departmentSearch
         * @created LHTDung
         * @date 24/09/2021
         */
        getValueDropdownSingle(e){
            if(e.length > 0){
                this.departmentSearch = e[0].text;
            }
            if(e == ""){
                this.departmentSearch = "";
            }
        },

        /**
         * @description Click vào radio "Đang áp dụng"
         * @created LHTDung
         * @date 23/09/2021
         */
        checkRadioAppling(){
            if(this.mode == this.$resourceVn.EditScreen){
                this.objectEdit.Status = this.$resourceVn.Status[1].Value;
            }
        },

        /**
         * @description Click vào radio "Ngừng áp dụng"
         * @created LHTDung
         * @date 23/09/2021
         */
        checkRadioStopApply(){
            if(this.mode == this.$resourceVn.EditScreen){
                this.objectEdit.Status = this.$resourceVn.Status[2].Value;
            }
        },

        /*********************************** GỌI API XÓA *********************************/
        /***
         * @description Gọi API thực hiện xóa chính sách
         * @created LHTDung
         * @date 24/09/2021
         */
        callAPIDelete(listId){
            axios.delete(this.$resourceVn.URL_POLICY_COMMON, { data: listId })
            .then(res => {
                console.log(res);
                this.callAPIFilterPolicy(this.pageSize, this.pageNumber, this.inputSearch, this.statusSearch, this.departmentSearch);
            }).then( () => {
                this.queueDelete = [];
                this.queueChecked = [];
                this.mode = this.$resourceVn.MainScreen;
                EventBus.$emit('showToast', this.$resourceVn.DeleteText, this.$resourceVn.TOAST_SUCCESS);
                
            })
            .catch(err => {
                console.error(err); 
            })
        },

        /**
         * @description Xử lý sự kiện Click button Xóa nhiều chính sách
         * @created LHTDung
         * @date 24/09/2021
         */
        clickDeleteMultiple(){
            //Đưa các Id của chính sách vào 1 mảng
            this.queueChecked.forEach(item => {
                this.queueDelete.push(item.PolicyId);
            });
            EventBus.$emit('showPopupDeleteMultiple', this.$resourceVn.POPUP_DELETE_MULTI_TYPE);
        },
        
        /**
         * @description Nhận $emit từ sự kiện click button xóa trên từng dòng của datagrid -> Lấy IdPolicy (Xóa đơn)
         * @created LHTDung
         * @date 24/09/2021
         */
        deleteSingleRow(data){
            //Đưa IdPolicy của dòng được click vào queueDelete 
            this.queueDelete.push(data.PolicyId);
        },

        /**
         * @description Nhận $emit từ sự kiện click button đồng ý "Xóa"
         * @created LHTDung
         * @date 24/09/2021
         */
        acceptDelete(){
            //Gọi API thực hiện xóa
            this.callAPIDelete(this.queueDelete);
        },

        /**
         * @description Xử lý sự kiện click Xóa trong màn xem chi tiết
         * @date 28/09/2021
         * @created LHTDung
         */
        handleDeleteInViewScreen(){
            this.queueDelete.push(this.objectEdit.PolicyId);
            this.clickedThreeDots = false;
            EventBus.$emit('showPopupDeleteSingle', this.objectEdit.PolicyName, this.$resourceVn.POPUP_DELETE_TYPE);
        },

        /**
         * @description Xử lý sự kiện click Nhân bản
         * @date 28/09/2021
         * @created LHTDung
         */
        handleDuplicatePolicy(){
            let duplicateObject = {...this.objectEdit};
            this.clickedThreeDots = false;
            // Gọi API thực hiện thêm mới - nhân bản
            axios.post(this.$resourceVn.URL_POLICY_COMMON, duplicateObject)
            .then(res => {
                console.log(res);
            })
            .then(() => {
                this.mode = this.$resourceVn.MainScreen;
                this.callAPIFilterPolicy(this.pageSize, this.pageNumber, this.inputSearch, this.statusSearch, this.departmentSearch);
                EventBus.$emit('showToast', this.$resourceVn.ButtonIconAddText, this.$resourceVn.TOAST_SUCCESS);
            })
            .catch(err => {
                console.error(err); 
            })
        },

        /**
         * @description Cập nhật lại listHeader của DataGrid, nhận $emit từ CustomeColumn
         * @created LHTDung
         * @date 24/09/2021
         */
        updateListHeaderGrid(newListHeader){
            let tempHeaders = [];
            newListHeader.forEach(item => {
                if(item.Checked == true){
                    tempHeaders.push(item);
                }
            });
            // Cập nhật listHeader
            this.headers = newListHeader;
            this.listHeader = tempHeaders;
            this.showPopupEditColumn = false;
        },

        /**
         * @description Cập nhật lại listHeader mặc định của DataGrid
         * @created LHTDung
         * @date 24/09/2021
         */
        updateListHeaderDefault(){
            this.headers = [...this.headersDefault]; 
            this.headers.forEach(item => {
                item.Checked = true;
            });
            this.listHeader = [...this.headersDefault];
            this.showPopupEditColumn = false;
        },

        /**
         * @description Clear check trong datagrid
         * @created LHTDung
         * @date 25/09/2021
         */
        clearSelectionDataGrid(){
            EventBus.$emit('clearSelectionDataGrid');
        },

        /**
         * @description Hiển thị tooltip
         * @created LHTDung
         * @date 28/09/2021
         */
        showTooltip(e){
            if(e.target.classList.contains('back')){
                EventBus.$emit('showTooltip', '#back' , this.$resourceVn.BACK_TEXT);
            }
            if(e.target.classList.contains('back-add')){
                EventBus.$emit('showTooltip', '#back-add', this.$resourceVn.BACK_TEXT);
            }
            if(e.target.classList.contains('filter')){
                EventBus.$emit('showTooltip', '#filter', this.$resourceVn.FilterText);
            }
            if(e.target.classList.contains('setting')){
                EventBus.$emit('showTooltip', '#setting-column', this.$resourceVn.SETTING_COLUMN);
            }
            if(e.target.classList.contains('save-tooltip')){
                EventBus.$emit('showTooltip', '.save-tooltip', this.$resourceVn.CTRL_S);
            }
        },

        /**
         * @description Ẩn tooltip
         * @created LHTDung
         * @date 28/09/2021
         */
        hideTooltip(){
            EventBus.$emit('hideTooltip');
        },

        /**
         * @description Xử lý sự kiện click button Lưu (Popup) - Lưu khi hủy form sửa
         * @date 28/09/2021
         * @created LHTDung
         */
        clickSaveEditPopup(){
            this.saveEdit();
        },

        /**
         * @description Xử lý sự kiện click button Lưu (Popup) - Lưu khi hủy form thêm mới
         * @date 28/09/2021
         * @created LHTDung
         */
        clickSaveAddPopup(){
            this.clickSave();
        },

        /**
         * @description Phím tắt Ctrl + S để "Lưu" dữ liệu
         * @date 28/09/2021
         * @created LHTDung
         */
        keyDoSave(e){
            if (!((e.keyCode === 83 && e.ctrlKey)|| e.keyCode === 27)) {
                return;
            }
            e.preventDefault();
            if(this.mode != this.$resourceVn.MainScreen){
                //Phím tắt ESC: Tắt form
                if(e.keyCode === 27){
                    this.exitModalBox();
                }

                //Phím tắt Ctrl + S : "Cất"
                if((e.keyCode === 83 && e.ctrlKey) && (this.modalBoxShow == false)){
                    console.log('save');
                    if(this.mode == this.$resourceVn.AddScreen){
                        this.clickSave();
                    }
                    if(this.mode == this.$resourceVn.EditScreen){
                        this.saveEdit();
                    }
                }
            }
        },

        /**
         * @description Bắt sự kiện click ngoài button 3 chấm -> Đóng group (nhân bản, xóa)
         * @date 28/09/2021
         * @created LHTDung
         */
        hideGroupThreeDots(){
            this.clickedThreeDots = false;
        }
        
    },

    watch:{
        mode: function(){
            //Thay đổi mode, khởi tạo lại các biển (validate)
            this.errorValidateInput = false;
            this.errorValidateDropdown = false;

            //Focus vào trường đầu tiên trong form 
            if(this.mode == this.$resourceVn.AddScreen){
                this.listChoosedComponent = [];
                this.$nextTick(() =>  {
                    this.focusFirstField();
                })
            }
            if(this.mode == this.$resourceVn.MainScreen){
                this.pageSize = 15;
                this.objectEdit = {};
            }
        },

        /**
         * @description Khi có sự thay đổi về số bản ghi/trang -> Gọi API phân trang, trở về trang đầu tiên
         * @created LHTDung
         * @date 23/09/2021
         */
        pageSize: function(){
            this.pageNumber = 1;
            this.callAPIFilterPolicy(this.pageSize, this.pageNumber, this.inputSearch, this.statusSearch, this.departmentSearch);
        },

        /**
         * @description Khi có sự thay đổi về trang -> Gọi API phân trang
         * @created LHTDung
         * @date 23/09/2021
         */
        pageNumber: function(){
            this.callAPIFilterPolicy(this.pageSize, this.pageNumber, this.inputSearch, this.statusSearch, this.departmentSearch);
        },
        
        /**
         * @description Khi có sự thay đổi về statusSearch(tìm theo trạng thái)
         * @created LHTDung
         * @date 23/09/2021
         */
        statusSearch: function(){
            this.pageNumber = 1;
            this.callAPIFilterPolicy(this.pageSize, this.pageNumber, this.inputSearch, this.statusSearch, this.departmentSearch);
        },

        /**
         * @description Khi có sự thay đổi về departmentSearch(tìm theo đơn vị)
         * @created LHTDung
         * @date 24/09/2021
         */
        departmentSearch: function(){
            this.callAPIFilterPolicy(this.pageSize, this.pageNumber, this.inputSearch, this.statusSearch, this.departmentSearch);
        },

        queueChecked: function(){
            let countActive = 0;
            let countDontActive = 0;
            for(let i = 0; i < this.queueChecked.length; i++){
                if(this.queueChecked[i].Status == this.$resourceVn.Status[1].Value){
                    countActive++;
                }else{
                    countDontActive++;
                }
            }
            if(countActive ==  this.queueChecked.length){
                this.showButtonStopApply = true;
                this.showButtonApply = false;
            }
            else if(countDontActive == this.queueChecked.length){
                this.showButtonApply = true;
                this.showButtonStopApply = false;
            }else{
                this.showButtonStopApply = true;
                this.showButtonApply = true;
            }
        }
    }
}
</script>
<style scoped>
    @import "../../css/layout/content.css";
    @import "../../css/view/salary/SalaryPolicyList.css";
    @import "../../css/base/validate-text/validate-text.css";
</style>