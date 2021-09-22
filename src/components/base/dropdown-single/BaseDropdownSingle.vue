<template>
    <div class="dx-field-value">
        <DxDropDownBox
          ref="dropdownBox"
          :value="treeBoxValue"
          :show-clear-button="true"
          :data-source="treeDataSource"
          value-expr="DepartmentId"
          display-expr="DepartmentName"
          :placeholder="this.$resourceVn.AllDepartment"
          @value-changed="syncTreeViewSelection($event)"
        >
          <template #content="{  }">
            <DxTreeView
              :ref="treeViewRefName"
              :data-source="treeDataSource"
              :select-by-click="true"
              :searchEnabled="true"
              data-structure="plain"
              key-expr="DepartmentId"
              parent-id-expr="ParentId"
              root-value="00000000-0000-0000-0000-000000000000"
              selection-mode="single"
              display-expr="DepartmentName"
              @content-ready="$event.component.selectItem(treeBoxValue)"
              @item-selection-changed="treeView_itemSelectionChanged($event)"
              @item-click="onTreeItemClick($event)"
            />
          </template>
        </DxDropDownBox>
    </div>
</template>
<script>
// import treeData from "../../../js/fake-data/data-dropdown-single"

import DxDropDownBox from 'devextreme-vue/drop-down-box';
import DxTreeView from 'devextreme-vue/tree-view';
export default {
    name: "BaseDropdownSingle",
    components: {
        DxDropDownBox,
        DxTreeView,
    },
    props:{
        treeDataSource:{
          type:Array,
          default(){
            return [];
          }
        }
    },
    data() {
        return {
            treeBoxValue: null,
            treeViewRefName: 'tree-view',
        };
    },
    
    methods: {
        /**
         * @Description Xử lý sự kiện khi item được click và bind dữ liệu
         * @Date 18/9/2021
         * @CreatedBy LHTDung
         */
        syncTreeViewSelection() {
          if (!this.$refs[this.textBoxRefName]) return;
          if (!this.treeBoxValue) {
            this.$refs[this.textBoxRefName].instance.unselectAll();
          } else {
            this.$refs[this.textBoxRefName].instance.selectItem(this.treeBoxValue);
          }
        },

        /**
         * @Description Xử lý sự kiện cho item được trọn trong tree data
         * @Date 18/9/2021
         * @CreatedBy LHTDung
         */
        treeView_itemSelectionChanged(e) {
          this.treeBoxValue = e.component.getSelectedNodeKeys();
        },

        /**
         * @Description Sau khi click item => ẩn dropdown
         * @Date 18/9/2021
         * @CreatedBy LHTDung
         */
        onTreeItemClick() {
          this.$refs.dropdownBox.instance.close();
        },
    },
}
</script>
<style scoped>
    @import "../../../css/base/dropdown-single/base-dropdown-single.css";
</style>